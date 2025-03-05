using Core;
using Cysharp.Threading.Tasks;
using Heroes.Model;
using SearchTeamFight.CharacterSystem.FighterEvents;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class FighterView : MonoBehaviour
    {
        [SerializeField] private FighterAnimatorEventDispatcher _fighterAnimatorEventDispatcher;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NavMeshObstacle _navMeshObstacle;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _smokeParticles;

        [SerializeField] private ParticleSystem _bloodParticles;
        [SerializeField] private Transform[] _creepsPositions;
        [SerializeField] private AbilityPopupView _abilityPopupView;
        [SerializeField] private FighterInfoBarView _fighterInfoBarView;
        [SerializeField] private FighterHealthBarView _fighterHealthBarView;
        [SerializeField] private HitBoxView _attackHitBoxView;
        [SerializeField] private CapsuleCollider _attackRangeCollider;
        [SerializeField] private CapsuleCollider _agentCollider;
        [SerializeField] private Transform _hitPosition;
        [SerializeField] private LayerMask _layerMask;

        [Header("Range attack effects")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private bool _isStaticPositionAttackEffect = false;
        [SerializeField] private bool _isStaticPositionWhileCast = false;
        [SerializeField] private bool _isLookAtTarget = true;
        [SerializeField] private float _delayStaticPOsitionWhileCast = 0;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private AnimationCurve _yPositionCurve;
        [SerializeField] private float _disableDelayTime;

        private float _yOffset = default;
        private bool _isStaticPosition = false;

        [Header("Meele attack effects")]
        [SerializeField] private List<GameObject> _meeleEffects = new();
        private int _enableWeaponAttackIndex = 0;
        private int _disableWeaponAttackIndex = 0;

        private CancellationTokenSource _cansellationTokenSource;

        private LauncherProjectileMover _launcherProjectileMover;
        private LauncherStaticProjectile _launcherStaticProjectile;

        private static GameObject _parentGameObjectForProjectilePool;

        private DraggableView _draggableView;

        public FighterAnimatorEventDispatcher FighterAnimatorEventDispatcher => _fighterAnimatorEventDispatcher;
        public Transform[] CreepsPositions => _creepsPositions;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public NavMeshObstacle NavMeshObstacle => _navMeshObstacle;
        public AbilityPopupView AbilityPopupView => _abilityPopupView;
        public FighterInfoBarView FighterInfoBarView => _fighterInfoBarView;
        public FighterHealthBarView FighterHealthBarView => _fighterHealthBarView;

        public DraggableView DraggableView
        {
            get
            {
                if(!IsDraggableView)
                    _draggableView = gameObject.AddComponent(typeof(DraggableView)) as DraggableView;
                return _draggableView;
            }
        }

        public bool IsDraggableView => _draggableView != null;
        public HitBoxView AttackHitBoxView => _attackHitBoxView;
        public CapsuleCollider AgentCollider => _agentCollider;
        public Transform HitPosition => _hitPosition;
        public Transform ShootPoint => _shootPoint;
        public Vector3 ShootPosition => ShootPoint.position;
        public GameObject ProjectilePrefab => _projectilePrefab;

        public float ProjectileSpeed => _projectileSpeed;
        public GameObject ExplosionPrefab => _explosionPrefab;
        public float DisableDelayTime => _disableDelayTime;
        public AnimationCurve YPositionCurve => _yPositionCurve;
        public Transform LookAtTarget => _lookAtTarget;
        private Transform _lookAtTarget;
        private ProjectileMover _projectileMover;
        private bool _isPushed;
        private bool _isNavMeshStopped;
        private bool _navMeshAgentLastStateIsActive;
        private float _pushSpeedMultiplier = 0.7f;

        private void Awake()
        {
            if(_projectilePrefab != null)
            {
                if(_parentGameObjectForProjectilePool == null)
                {
                    _parentGameObjectForProjectilePool = new GameObject("ProjectilePool");
                }

                if (_isStaticPositionAttackEffect)
                {
                    _launcherStaticProjectile = new LauncherStaticProjectile(this, _parentGameObjectForProjectilePool);
                }
                else
                {
                    _launcherProjectileMover = new LauncherProjectileMover(this, _parentGameObjectForProjectilePool);
                }
            }
        }

        public async UniTask LaunchAttackProjectile(Transform target)
        {
            if (_projectilePrefab != null)
            {
                if (_isStaticPositionAttackEffect)
                {
                    await _launcherStaticProjectile.Launch(target, destroyCancellationToken);
                }
                else
                {
                    await _launcherProjectileMover.Launch(target, destroyCancellationToken);
                }
            }
        }

        public void PlayIdleAnimation()
        {
            _animator.SetTrigger("Idle");
        }

        public void SetAttackRange(float range) => 
            _attackRangeCollider.radius = range;

        public void ShowMeleeEffects(bool isShow)
        {
            if (isShow)
            {
                if(_enableWeaponAttackIndex > _meeleEffects.Count - 1)
                {
                    _enableWeaponAttackIndex = 0;
                }
                _meeleEffects[_enableWeaponAttackIndex].SetActive(true);
                _enableWeaponAttackIndex++;
            }
            else
            {
                if(_disableWeaponAttackIndex > _meeleEffects.Count - 1)
                {
                    _disableWeaponAttackIndex = 0;
                }
                _meeleEffects[_disableWeaponAttackIndex].SetActive(false);
                _disableWeaponAttackIndex++;
            }
        }

        public void PlayBloodParticles()
        {
            _bloodParticles.Play();
        }

        public async UniTask MoveTo(Transform target, float stopDistance, CancellationToken token)
        {
            while (_isNavMeshStopped)
                await UniTask.DelayFrame(1, cancellationToken: token);

            if (token.IsCancellationRequested)
                return;

            var distance = (target.position.xz() - _navMeshAgent.transform.position.xz()).magnitude;

            if(distance <= stopDistance)
            {
                if(_navMeshAgent.enabled && gameObject.activeSelf)
                {
                    _navMeshAgent.isStopped = true;
                }

                _navMeshAgent.enabled = false;

                _navMeshObstacle.enabled = false;

                return;
            }

            _animator.SetTrigger("Walk");

            _navMeshObstacle.enabled = false;
            _navMeshAgent.stoppingDistance = 0;

            _navMeshAgent.enabled = true;
            _navMeshAgent.isStopped = false;

            while (distance > stopDistance && !_isStaticPosition && !token.IsCancellationRequested)
            {
                if (!_isNavMeshStopped)
                {
                    distance = (target.position.xz() - _navMeshAgent.transform.position.xz()).magnitude;

                    _navMeshAgent.SetDestination(target.position);
                }

                await UniTask.DelayFrame(1, cancellationToken: token);
            }

            if (token.IsCancellationRequested)
                return;

            _navMeshAgent.isStopped = true;
            _navMeshAgent.enabled = false;

            _navMeshObstacle.enabled = true;
        }

        public void SetLookAtTarget(Transform lookAt)
        {
            _lookAtTarget = lookAt;
        }

        public async UniTask PlayAttackAnimationAsync(float attackSpeed, CancellationToken token)
        {
            _animator.SetTrigger("Attack");

            await UniTask.DelayFrame(1, cancellationToken: token);

            var currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var attackAnimationDuration = currentStateInfo.length;
            var animationAcceleration = attackAnimationDuration / attackSpeed;
            var newAttackAnimationDuration = attackAnimationDuration / animationAcceleration;

            var initialAnimatorSpeed = _animator.speed;

            _animator.speed *= animationAcceleration;

            var registration = token.Register(() => _animator.speed = initialAnimatorSpeed);

            await UniTask.Delay(TimeSpan.FromSeconds(newAttackAnimationDuration), cancellationToken: token);

            registration.Dispose();

            _animator.speed = initialAnimatorSpeed;
        }

        public bool IsInRange(Transform target, float rangeDistance)
        {
            var distance = (target.position.xz() - _navMeshAgent.transform.position.xz()).magnitude;

            return rangeDistance > distance;
        }

        public void StopNavMeshAgentState()
        {
            if(_isNavMeshStopped) 
                return;

            _isNavMeshStopped = true;
            _navMeshAgentLastStateIsActive = _navMeshAgent.enabled;

            if (_navMeshAgent.enabled)
                _navMeshAgent.isStopped = true;

            _navMeshAgent.enabled = false;
            _navMeshObstacle.enabled = false;
        }

        public void ReturnNavMeshAgentState()
        {
            if (!_isNavMeshStopped)
            {
                return;
            }

            if (_navMeshAgentLastStateIsActive)
            {
                _navMeshObstacle.enabled = false;
                _navMeshAgent.enabled = true;
                _navMeshAgent.isStopped = false;
            }

            _isNavMeshStopped = false;
        }

        public void PlayWinAnimation()
        {
            DisableMeleeEffects();
            _animator.SetTrigger("Win");
        }

        public async void PlayDeathAnimation()
        {
            _attackHitBoxView.gameObject.SetActive(false);
            DisableMeleeEffects();

            _animator.SetTrigger("Death");

            await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: destroyCancellationToken)
                .ContinueWith(() =>
                {
                    gameObject.SetActive(false);
                });
        }

        private void DisableMeleeEffects()
        {
            foreach (var meleeEffect in _meeleEffects)
            {
                if(meleeEffect != null)
                    meleeEffect.SetActive(false);
            }
        }
    }
}
