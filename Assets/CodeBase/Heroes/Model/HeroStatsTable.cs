using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Heroes.Model
{
    [CreateAssetMenu(menuName = "Heroes/Hero Stats Table")]
    public class HeroStatsTable : ScriptableObject
    {
        public IReadOnlyList<HeroStatsTableItem> Items => _items;

        [SerializeField] private HeroStatsTableItem[] _items;

        public void Replase(HeroStatsTableItem[] items) =>
            _items = items;
    }

    [Serializable]
    public class HeroStatsTableItem
    {
        public int Strength => _strength;

        public int Attack => _attack;
        public int Defense => _defense;
        public int Health => _health;

        public int ExpeditionArmySize => _expeditionArmySize;
        public float ExpeditionAttackBonus => _expeditionAttackBonus;
        public float ExpeditionDefenseBonus => _expeditionDefenseBonus;

        public float CritRate => _critRate;
        public float CritScale => _critScale;


        [SerializeField] private int _strength;

        [Space]
        [SerializeField] private int _attack;
        [SerializeField] private int _defense;
        [SerializeField] private int _health;

        [Space]
        [SerializeField, Range(0.0f, 100.0f)] private float _critRate = 0.0f;
        [SerializeField, Range(0.0f, 5.0f)] private float _critScale = 2.0f;

        [Space]
        [SerializeField] private int _expeditionArmySize;
        [SerializeField] private float _expeditionAttackBonus;
        [SerializeField] private float _expeditionDefenseBonus;

        public HeroStatsTableItem WithStrength(int strength)
        {
            _strength = strength;
            return this;
        }

        public HeroStatsTableItem WithAttack(int attack)
        {
            _attack = attack;
            return this;
        }

        public HeroStatsTableItem WithDefense(int defense)
        {
            _defense = defense;
            return this;
        }

        public HeroStatsTableItem WithHealth(int health)
        {
            _health = health;
            return this;
        }

        public HeroStatsTableItem WithCritRate(float critRate)
        {
            _critRate = critRate;
            return this;
        }

        public HeroStatsTableItem WithCritScale(float critScale)
        {
            _critScale = critScale;
            return this;
        }
    }
}
