using Cysharp.Threading.Tasks;

namespace SearchTeamFight.CharacterSystem.StateMachine.States
{
    public class WinState : FighterState
    {
        public override UniTask Enter()
        {
            FighterView.NavMeshAgent.enabled = false;
            FighterView.PlayWinAnimation();
            return UniTask.CompletedTask;
        }

        public override void Exit() { }

        public override void Dispose() { }
    }
}
