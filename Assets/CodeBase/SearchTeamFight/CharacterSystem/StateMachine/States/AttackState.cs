using Cysharp.Threading.Tasks;
using SearchTeamFight.CharacterSystem.Services;
using System.Threading;
using System;
using Zenject;
using UniRx;
using UnityEngine;
using SearchTeamFight.CharacterSystem.Models;
using System.Linq;
using System.Security.Cryptography;
using SearchTeamFight.CharacterSystem.Views.WorldMap;
using System.Threading.Tasks;

namespace SearchTeamFight.CharacterSystem.StateMachine.States
{
    public class AttackState : FighterState
    {
        [Inject] private FightersContainer _fightersContainer;

        private CancellationTokenSource _stateCycleCts;
        private IDisposable _newTargetDisposable;

        public override UniTask Enter()
        {
            _stateCycleCts = new();

            FighterView.transform.SetParent(null);

            FighterView.NavMeshAgent.enabled = true;
            FighterView.AgentCollider.enabled = true;
            FighterView.NavMeshObstacle.enabled = true;

            FighterView.SetAttackRange(FighterModel.Data.Stats.AttackRange);

            if (FighterModel.Data.Stats.IsMelee)
            {
                FighterView.FighterAnimatorEventDispatcher.OnAttackPerformed
                    .Subscribe(_ => OnMeleeAttackStart())
                    .AddTo(StateDisposables);
                FighterView.FighterAnimatorEventDispatcher.OnAttackEnded
                    .Subscribe(_ => OnMeleeAttackEnd())
                    .AddTo(StateDisposables);
            }
            else
            {
                FighterView.FighterAnimatorEventDispatcher.OnAttackPerformed
                    .Subscribe(_ => OnRangeAttackEnd())
                    .AddTo(StateDisposables);
            }

            StartStateCycle(_stateCycleCts.Token).Forget();

            return UniTask.CompletedTask;
        }

        public override void Exit()
        {
            _stateCycleCts.Cancel();
            _stateCycleCts.Dispose();
        }
        public override void Dispose()
        {
            _stateCycleCts?.Cancel();
            _stateCycleCts?.Dispose();
        }
        
        private void OnMeleeAttackEnd()
        {
            var attackReceiverModel = FighterModel.Target.Value;
            var attackDealerModel = FighterModel;

            var attackReceiverView = _fightersContainer.FighterViewByModel[attackReceiverModel];
            var attackDealerView = _fightersContainer.FighterViewByModel[attackDealerModel];

            attackDealerView.ShowMeleeEffects(false);

            attackReceiverModel.SubstractHealth(
                Mathf.RoundToInt(attackDealerModel.Attack.Value.Value),
                attackDealerModel.Data.Stats,
                attackReceiverModel.Data.Stats
                );

            attackReceiverView.PlayBloodParticles();
            attackDealerModel.OnAttack.Execute(attackReceiverModel);
        }

        public void OnMeleeAttackStart()
        {
            var attackDealerModel = FighterModel;
            var attackDealerView = _fightersContainer.FighterViewByModel[attackDealerModel];

            attackDealerView.ShowMeleeEffects(true);
        }
        
        private async void OnRangeAttackEnd()
        {
            var attackReceiverModel = FighterModel.Target.Value;
            var attackDealerModel = FighterModel;

            var attackReceiverView = _fightersContainer.FighterViewByModel[attackReceiverModel];
            var attackDealerView = _fightersContainer.FighterViewByModel[attackDealerModel];

            await attackDealerView.LaunchAttackProjectile(attackReceiverView.HitPosition);

            attackReceiverModel.SubstractHealth(
                Mathf.RoundToInt(attackDealerModel.Attack.Value.Value),
                attackDealerModel.Data.Stats,
                attackReceiverModel.Data.Stats
                );

            attackReceiverView.PlayBloodParticles();
            attackDealerModel.OnAttack.Execute(attackReceiverModel);
        }

        private async UniTask StartStateCycle(CancellationToken token)
        {
            var target = GetNewTraget();

            if (target == null)
                return;

            FighterModel.SetTarget(target);

            var targetView = _fightersContainer.FighterViewByModel[target];

            _newTargetDisposable = FighterView.AttackHitBoxView.OnEnter
                .Subscribe(OnNewTargetEntered)
                .AddTo(StateDisposables);

            await FighterView.MoveTo(targetView.NavMeshAgent.transform, FighterModel.AttackRange, token);

            _newTargetDisposable.Dispose();

            await StartAttackCycle(token);  
        }

        private async UniTask StartAttackCycle(CancellationToken token)
        {
            var attackReceiverModel = FighterModel.Target.Value;
            var attackDealerModel = FighterModel;

            var attackReceiverView = _fightersContainer.FighterViewByModel[attackReceiverModel];
            var attackDealerView = _fightersContainer.FighterViewByModel[attackDealerModel];

            attackDealerView.SetLookAtTarget(attackReceiverView.NavMeshAgent.transform);
            StartRangeCheckCycle(token).Forget();

            while (attackReceiverModel.CurrentHealth > 0 && !token.IsCancellationRequested)
            {
                await attackDealerView.PlayAttackAnimationAsync(attackDealerModel.AttackRate.Value.Value, token);
            }
        }

        private async UniTask StartRangeCheckCycle(CancellationToken token)
        {
            var attackReceiverModel = FighterModel.Target.Value;
            var attackDealerModel = FighterModel;

            var attackReceiverView = _fightersContainer.FighterViewByModel[attackReceiverModel];
            var attackDealerView = _fightersContainer.FighterViewByModel[attackDealerModel];

            while (!token.IsCancellationRequested)
            {
                if(attackDealerView.IsInRange(
                    attackReceiverView.NavMeshAgent.transform, 
                    attackDealerModel.AttackRange + 0.1f))
                {
                    await UniTask.DelayFrame(1, cancellationToken: token);
                }
                else
                {
                    _stateCycleCts.Cancel();
                    _stateCycleCts.Dispose();
                    _stateCycleCts = new();

                    StartStateCycle(_stateCycleCts.Token).Forget();
                    return;
                }
            }
        }

        private void OnNewTargetEntered(GameObject gameObject)
        {
            var enteredFighterView = gameObject.transform.parent.GetComponent<FighterView>();
            var enteredFighterModel = _fightersContainer.FighterModelByView[enteredFighterView];

            if(enteredFighterModel.Team != FighterModel.Team && enteredFighterModel != FighterModel.Target.Value)
            {
                _newTargetDisposable?.Dispose();

                _stateCycleCts.Cancel();
                _stateCycleCts.Dispose();
                _stateCycleCts = new();

                StartStateCycle(_stateCycleCts.Token).Forget();
            }
        }

        private FighterModel GetNewTraget()
        {
            var enemyTeam = FighterModel.Team.GetOppositeTeam();
            var enemiesModels = _fightersContainer.GetFighterModelByTeam(enemyTeam).ToList();
            enemiesModels.RemoveAll(enemy => enemy.IsDead);

            if(enemiesModels.Count == 0)
            {
                return null;
            }

            var enemiesViewsData = enemiesModels.Select(model =>
                {
                    var view = _fightersContainer.FighterViewByModel[model];
                    var distance = Vector3.Distance(view.NavMeshAgent.transform.position,
                        FighterView.NavMeshAgent.transform.position);

                    return (model, view, distance);
                })
                .OrderBy(tuple => tuple.distance)
                .ToArray();

            var result = enemiesViewsData
                        .FirstOrDefault(
                            tuple => !tuple.model.IsCreep && tuple.distance < FighterModel.AttackRange).model
                        ?? enemiesViewsData.FirstOrDefault(tuple => tuple.distance < FighterModel.AttackRange).model
                        ?? enemiesViewsData.FirstOrDefault(tuple => !tuple.model.IsCreep).model
                        ?? enemiesViewsData.FirstOrDefault().model;

            return result;
        }
    }
}
