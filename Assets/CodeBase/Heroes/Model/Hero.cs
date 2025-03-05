using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace Heroes.Model
{
    [Serializable]
    public class Hero
    {
        public EHero ID => _id;
        public EHeroClass Class => _config.Class;
        public EHeroRarity Rarity => _config.Rarity;
        public string Name => _config.Name;
        public HeroStats Stats => _stats;

        public IntReactiveProperty ReactiveLevel
        {
            get
            {
                if (_level.Value == 0)
                {
                    _level.Value = 1;
                }

                return _level;
            }
        }

        public int Level
        {
            get => ReactiveLevel.Value;

            set
            {
                _level.Value = Mathf.Clamp(value, 1, _config.StatsTable.Items.Count);
                CalculateStats();
            }
        }

        [SerializeField] private EHero _id;
        [HideInInspector][SerializeField] private bool _isOnMap = false;
        [HideInInspector][SerializeField] private BoolReactiveProperty _hired = new(false);

        [SerializeField] private IntReactiveProperty _level = new(1);
        /*[HideInInspector][SerializeField] private ReactiveDictionary<EGearType, IGearItem> _equippedGear = new();*/

        [NonSerialized] private HeroConfig _config;
        [NonSerialized] private HeroesConfig _generalConfig;
        [NonSerialized] private HeroStats _stats;
        [NonSerialized] private ReactiveProperty<HeroStats> _reactiveStats;

        private List<HeroStatsTableItem> _serverHeroStatsTable = new();

        private CompositeDisposable _disposables = new();

        public Hero(
            EHero id, 
            bool isOnMap, 
            bool hired, 
            int level
            /*Dictionary<EGearType, IGearItem> equippedGear*/)
        {
            _id = id;
            _isOnMap = isOnMap;
            _hired = new(hired);
            _level = new(Mathf.Max(level, 1));
            /*_equippedGear = new(equippedGear ?? new());*/
            _reactiveStats = new(new HeroStats());
        }

        public Hero(EHero id)
        {
            _id = id;
            _level = new(1);
            _reactiveStats = new(new HeroStats());
        }

        public void ChangeId(EHero id) => 
            _id = id;

        public void Initialize(HeroesConfig generalConfig)
        {
            if(generalConfig.Get(_id) == null)
            {
                Debug.LogWarning("General config for hero is null");
                return;
            }

            if(_config != null)
            {
                Debug.LogWarning("Hero already initialized");
            }
            else
            {
                _config = GameObject.Instantiate(generalConfig.Get(_id));
                _generalConfig = generalConfig;
            }

            /*var isEnemy = IsEnemyTeam((int)_id);
            if (!isEnemy)
            {
                if(_disposables == null)
                    _disposables = new();

                
            }*/

            if (_reactiveStats == null)
                _reactiveStats = new();

            CalculateStats();
        }

        private void CalculateStats()
        {
            if (_level.Value <= 0)
            {
                _level.Value = 1;
                Debug.LogError($"[Hero] CalculateStats; Level is {_level.Value} set 1");
            }

            HeroStatsTableItem curStats = default;

            curStats = _config.StatsTable.Items[_level.Value - 1];

            _stats = new HeroStats
            {
                Class = _config.Class,

                /*// TODO: add total abilities strength
                Strength = curStats.Strength + curStarStats.Strength + heroGearsStrength,*/
                
                Strength = curStats.Strength,

                AbilitiesStrength = curStats.Strength,

                /*Attack = curStats.Attack + curStarStats.AttackBonus + heroAttackBonusFromGear,
                Defense = curStats.Defense + curStarStats.DefenseBonus + heroDefenseBonusFromGear,
                Health = curStats.Health + curStarStats.HealthBonus + heroHealthBonusFromGear,*/

                Attack = curStats.Attack,
                Defense = curStats.Defense,
                Health = curStats.Health,

                MarchSpeed = 1,

                CritRate = 0.0f,
                CritScale = 2.0f,

                AttackRate = _config.AttackRate,
                AttackRange = _generalConfig.GetAttackRange(_config.AttackRangeType),
                IsMelee = _config.Class == EHeroClass.Tank,

                ExpeditionArmySize = curStats.ExpeditionArmySize,
                ExpeditionAttackBonus = curStats.ExpeditionAttackBonus,
                ExpeditionDefenseBonus = curStats.ExpeditionDefenseBonus
            };

            _reactiveStats.SetValueAndForceNotify(_stats);
        }

        private bool IsEnemyTeam(int id)
        {
            return id is > 2 and < 5;
        }

        public Hero Clone() => 
            new(_id, _isOnMap, _hired.Value, _level.Value);
    }

    public struct HeroStats
    {
        public EHeroClass Class;

        public int Strength;
        public int AbilitiesStrength;

        public int Attack;
        public int Defense;
        public int Health;
        public float MarchSpeed;

        public float CritRate;
        public float CritScale;

        public float AttackRate;
        public float AttackRange;
        public bool IsMelee;

        public int ExpeditionArmySize;
        public float ExpeditionAttackBonus;
        public float ExpeditionDefenseBonus;
    }
}
