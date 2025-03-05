namespace SearchTeamFight.CharacterSystem.Models.Stats
{
    public class FighterFloatStat : AFighterStat<float>
    {
        public FighterFloatStat(float baseValue, float multiplier = 1) : base(baseValue, multiplier) { }

        protected override float Add(float a, float b) => a + b;

        protected override float Mul(float a, float b) => a * b;

        protected override float Neg(float value) => -value;
    }
}
