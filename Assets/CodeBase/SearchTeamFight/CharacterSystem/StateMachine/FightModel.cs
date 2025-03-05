using UniRx;

namespace SearchTeamFight.CharacterSystem.StateMachine
{
    public class FightModel
    {
        public readonly ReactiveCommand OnFightStarted = new();
        public readonly ReactiveCommand OnFightStartedLate = new();
        public readonly ReactiveCommand OnFightEnded = new();
    }
}
