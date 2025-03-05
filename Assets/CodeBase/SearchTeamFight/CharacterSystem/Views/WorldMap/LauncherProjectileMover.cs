using Core.Utils;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using UniRx;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class LauncherProjectileMover
    {
        private readonly ObjectPool<ProjectileMover> _pool;
        private readonly FighterView _fighterView;

        public LauncherProjectileMover(FighterView fighterView, GameObject projectilePoolGameObject)
        {
            _fighterView = fighterView;

            var parentGameObject = new GameObject(fighterView.name);

            if (projectilePoolGameObject != null)
                parentGameObject.transform.SetParent(projectilePoolGameObject.transform, false);

            _pool = new ObjectPool<ProjectileMover>(
                createFunc: () =>
                {
                    var projectile = Object.Instantiate(
                        fighterView.ProjectilePrefab,
                        fighterView.ShootPoint.position,
                        Quaternion.identity,
                        parentGameObject.transform);
                    return projectile.AddComponent<ProjectileMover>();
                },
                actionOnGet: projectile =>
                {
                    if (projectile != null)
                    {
                        projectile.transform.position = fighterView.ShootPoint.position;
                        projectile.gameObject.SetActive(true);
                    }
                },
                actionOnRelease: projectile =>
                {
                    if (projectile != null)
                    {
                        projectile.gameObject.SetActive(false);
                    }
                },
                actionOnDestroy: projectile =>
                {
                    if (projectile != null)
                    {
                        Object.Destroy(projectile.gameObject);
                    }
                },
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        public async UniTask Launch(Transform target, CancellationToken token)
        {
            ProjectileMover projectile = _pool.Get();
            if(projectile == null)
            {
                _pool.Clear();
                projectile = _pool.Get();
            }

            projectile.transform.LookAt(target);

            projectile.gameObject.PlayParticleSystems();

            projectile.Initialize(
                target,
                _fighterView.ProjectileSpeed,
                _fighterView.DisableDelayTime,
                _fighterView.LookAtTarget,
                token);


            projectile.OnPositionUpdated += OnProjectilePositionUpdated;

            projectile.OnTargetReaced
                .Subscribe(position =>
                {
                    OnProjectileTargetReached(position);
                });

            projectile.OnFinish
                .Subscribe(projectile =>
                {
                    FinishProjectile(projectile);
                });
        }


        public void OnProjectileTargetReached(Vector3 position)
        {
            if (_fighterView.ExplosionPrefab == null)
                return;

            var exploisonEffect = Object.Instantiate(_fighterView.ExplosionPrefab, position, Quaternion.identity);
            Object.Destroy(exploisonEffect, 2.0f);
        }

        private Vector3 OnProjectilePositionUpdated(Vector3 position, Vector3 startPosition, float flightProgress) =>
            new(position.x, position.y * _fighterView.YPositionCurve.Evaluate(flightProgress), position.z);

        private void FinishProjectile(ProjectileMover projectileMover) => 
            _pool.Release(projectileMover);
    }
}