using Cysharp.Threading.Tasks;
using NUnit.Framework.Constraints;
using SearchTeamFight.CharacterSystem.Models;
using SearchTeamFight.CharacterSystem.StateMachine.States;
using SearchTeamFight.CharacterSystem.Views.WorldMap;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SearchTeamFight.CharacterSystem.StateMachine
{
    public abstract class FighterState : IDisposable
    {
        [Inject] protected FighterModel FighterModel;
        [Inject] protected FighterView FighterView;
        [Inject] protected FighterStateMachine FighterStateMachine;

        protected CancellationTokenSource StateCts;
        protected CompositeDisposable StateDisposables;

        public GameObject ContextForDebug => FighterView.gameObject;

        public abstract UniTask Enter();
        public abstract void Exit();
        public abstract void Dispose();

        public void BaseEnter()
        {
            StateCts = new();
            StateDisposables = new();

            FighterModel.OnCurrentHealthChanged
                .Subscribe(OnCurrentHealthChanged)
                .AddTo(StateDisposables);

            FighterModel.OnTeamWon
                .Subscribe(_ =>
                {
                    OnTeamWon();
                })
                .AddTo(StateDisposables);
        }

        public void BaseExit()
        {
            StateCts.Cancel();
            StateCts.Dispose();
            StateCts = null;

            StateDisposables.Dispose();
        }

        public void BaseDispose()
        {
            StateCts?.Cancel();
            StateCts?.Dispose();

            StateDisposables?.Dispose();
        }

        private void OnCurrentHealthChanged(int health)
        {
            if (health <= 0)
                FighterStateMachine.ChangeState<DeathState>();
        }

        private void OnTeamWon()
        {
            FighterStateMachine.ChangeState<WinState>();
        }
    }
}
