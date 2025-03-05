using Cysharp.Threading.Tasks;
using System;

namespace SearchTeamFight.CharacterSystem.StateMachine.States
{
    public class StunnedState : FighterState
    {
        private float _duration;

        public override async UniTask Enter()
        {
            FighterView.PlayIdleAnimation();
            FighterView.StopNavMeshAgentState();

            await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: StateCts.Token);

            if (StateCts.Token.IsCancellationRequested)
            {
                return;
            }

            FighterView.ReturnNavMeshAgentState();

            FighterStateMachine.ChangeState<AttackState>();
        }


                /*public struct BigStruct
                {
                    public int A, B, C, D, E, F, G, H;
                }

                // Обычная передача (копирование всей структуры)
                public void Process(BigStruct data) { }

                // Передача по ссылке только для чтения (без копирования)
                public void Process(in BigStruct data) { }*/


        public void SetDuration(in float duration) =>
            _duration = duration;

        public override void Exit() { }

        public override void Dispose() { }
    }
}
