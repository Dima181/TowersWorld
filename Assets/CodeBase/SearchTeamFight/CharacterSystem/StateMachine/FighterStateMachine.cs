using Cysharp.Threading.Tasks;
using SearchTeamFight.CharacterSystem.StateMachine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace SearchTeamFight.CharacterSystem.StateMachine
{
    public class FighterStateMachine : IDisposable
    {
        [Inject] private DiContainer _diContainer;

        private List<FighterState> _residentStates;

        private FighterState _currentState;

        public FighterState CurrentState => _currentState;
        public string CurrentStateName => _currentState.GetType().Name;

        public void ResolveStates() => 
            _residentStates = _diContainer.ResolveAll<FighterState>();

        public void Dispose()
        {
            _currentState?.BaseDispose();
            _currentState?.Dispose();
        }

        public void ChangeState(FighterState newState)
        {
            if(newState == _currentState || _currentState is DeathState)
                return;

            _currentState?.BaseExit();
            _currentState?.Exit();
            
            _currentState = newState;
            _currentState.BaseEnter();
            _currentState.Enter().Forget();
        }

        public void ChangeState<T>()
            where T : FighterState => 
                ChangeState(GetState<T>());

        private T GetState<T>()
            where T : FighterState =>
                _residentStates.First(s => s.GetType() == typeof(T)) as T;
    }
}
