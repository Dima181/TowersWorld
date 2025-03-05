using Cysharp.Threading.Tasks;
using SearchTeamFight.CharacterSystem.Models;
using SearchTeamFight.CharacterSystem.Services;
using SearchTeamFight.CharacterSystem.StateMachine;
using SearchTeamFight.CharacterSystem.StateMachine.States;
using SearchTeamFight.CharacterSystem.Views.WorldMap;
using System;
using System.Threading;
using UniRx;
using UnityEngine.Experimental.GlobalIllumination;
using Zenject;

namespace SearchTeamFight.CharacterSystem.Controllers
{
    public class FighterController : IInitializable, IDisposable
    {
        [Inject] private FighterModel _fighterModel;
        [Inject] private FighterView _fighterView;
        [Inject] private FighterStateMachine _fighterStateMachine;
        [Inject] private readonly FightModel _fightModel;

        [Inject] private FightersContainer _fightersContainer;

        private readonly CancellationTokenSource _fighterControllerCancellationTokenSource = new();

        private CompositeDisposable _compositeDisposable;

        private const float DelayHideHealthBarFighterView = 0f;
        private const float DelayHideHealthBarCreepView = 3.5f;

        public void Initialize()
        {
            _compositeDisposable = new();

            _fighterStateMachine.ResolveStates();
            _fightersContainer.AddFighter(_fighterModel, _fighterView, _fighterStateMachine);
            _fighterStateMachine.ChangeState<IdleState>();

            _fighterView.FighterInfoBarView.Setup(_fighterModel);

            _fighterView.FighterHealthBarView.Setup(_fighterModel.MaxHealth);

            if (_fighterModel.IsCreep)
            {
                _fighterView.FighterHealthBarView.Hide();
            }

            _fighterModel.OnCurrentHealthChanged
                .Subscribe(UpdateHealthBar)
                .AddTo(_compositeDisposable);

            _fightModel.OnFightStartedLate
                .Subscribe(_ => _fighterView.FighterInfoBarView.SetVisible(false))
                .AddTo(_compositeDisposable);
        }
        
        public void Dispose()
        {
            _fightersContainer.RemoveFighter(_fighterModel, _fighterView, _fighterStateMachine);

            _compositeDisposable.Dispose();
            _fighterControllerCancellationTokenSource.Cancel();
        }

        private void UpdateHealthBar(int health)
        {
            _fighterView.FighterHealthBarView.SetValue(health);

            if(health < 0)
            {
                _fighterStateMachine.ChangeState<DeathState>();
                HideFighterHealthBarViewAfterDelay(DelayHideHealthBarFighterView).Forget();
            }

            if (_fighterModel.IsCreep)
            {
                _fighterView.FighterHealthBarView.Show();
                HideFighterHealthBarViewAfterDelay(DelayHideHealthBarCreepView).Forget();
            }
        }

        private async UniTask HideFighterHealthBarViewAfterDelay(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _fighterControllerCancellationTokenSource.Token);
            _fighterView.FighterHealthBarView.Hide();
        }
    }
}
