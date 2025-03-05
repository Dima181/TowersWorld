using Cysharp.Threading.Tasks;

namespace SearchTeamFight.CharacterSystem.StateMachine.States
{
    public class IdleState : FighterState
    {
        public override UniTask Enter()
        {
            FighterView.PlayIdleAnimation();
            FighterView.NavMeshAgent.enabled = false;
            FighterView.AgentCollider.enabled = false;
            FighterView.NavMeshObstacle.enabled = false;

            return UniTask.CompletedTask;
        }

        public override void Exit() { }

        public override void Dispose() { }
    }
}
