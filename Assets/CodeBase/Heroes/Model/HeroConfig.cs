using UnityEngine;

namespace Heroes.Model
{
    [CreateAssetMenu(menuName = "Heroes/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        public EHero ID => _id;
        public EHeroClass Class => _class;
        public EHeroRarity Rarity => _rarity;
        public string Name => _name;
        public float AttackRate => _attackRate;
        public EAttackRange AttackRangeType => _attackRangeType;
        public HeroStatsTable StatsTable => _statsTable;

        [SerializeField] private EHero _id;
        [SerializeField] private EHeroClass _class;
        [SerializeField] private EHeroRarity _rarity;

        [SerializeField] private string _name;
        [SerializeField] private float _attackRate;
        [SerializeField] private EAttackRange _attackRangeType;
        [SerializeField] private HeroStatsTable _statsTable;

        public void ReplaceClass(EHeroClass heroClass) => _class = heroClass;
        public void ReplaceRarity(EHeroRarity rarity) => _rarity = rarity;
        public void ReplaceName(string name) => _name = name;
        public void ReplaceAttackRate(float attackRate) => _attackRate = attackRate;
    }
}
