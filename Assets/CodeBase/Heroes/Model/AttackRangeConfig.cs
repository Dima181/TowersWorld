using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

namespace Heroes.Model
{
    [CreateAssetMenu(menuName = "Configs/Heroes/" + nameof(AttackRangeConfig), fileName = nameof(AttackRangeConfig))]
    public class AttackRangeConfig : ScriptableObject
    {
        public SerializedDictionary<EAttackRange, float> AttackRangeByType => _attackRangeByType;

        [SerializedDictionary("Fighter Attack Type", "Range"), SerializeField]
        private SerializedDictionary<EAttackRange, float> _attackRangeByType;

    }
}
