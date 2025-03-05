using Cysharp.Threading.Tasks;

namespace SearchTeamFight.CharacterSystem.StateMachine.States
{
    public class DeathState : FighterState
    {
        public override UniTask Enter()
        {
            FighterView.StopNavMeshAgentState();
            FighterView.SetLookAtTarget(null);
            FighterView.PlayDeathAnimation();

            UnityEngine.Debug.Log($"[FightLog] Death {FighterModel.Team} => {FighterView.gameObject.name}");
            return UniTask.CompletedTask;
        }

        public override void Dispose() { }
        public override void Exit() { }
    }
}
