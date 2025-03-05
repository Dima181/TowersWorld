using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Heroes.Model
{
    [CreateAssetMenu(menuName = "Towers World/Characters Config")]
    public class HeroesConfig : ScriptableObject
    {
        public IReadOnlyList<EHero> FirstHeroes => _firstHeroes;
        public IReadOnlyList<HeroConfig> Configs => _list;

        [SerializeField] private EHero[] _firstHeroes;
        [SerializeField] private HeroConfig[] _list;
        [SerializeField] private AttackRangeConfig _attackRangeConfig;

        public HeroConfig Get(EHero hero) =>
            _list.FirstOrDefault(h => h.ID == hero);

        public bool Contains(EHero hero) =>
            _list.Any(config => config.ID == hero);

        public float GetAttackRange(EAttackRange attackRange)
        {
            if (_attackRangeConfig.AttackRangeByType.ContainsKey(attackRange))
                return _attackRangeConfig.AttackRangeByType[attackRange];
            return 5f;
        }
    }
}
