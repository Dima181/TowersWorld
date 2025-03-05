using Core;
using Heroes.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SearchTeamFight.Data
{
    [CreateAssetMenu(fileName = "AutoBattlerLevels", menuName = "AutoBattlerLevelSystem/AutoBattlerLevels")]
    public class AutoBattlerLevels : ScriptableObject
    {
        public List<AutoBattlerLevel> Levels = new List<AutoBattlerLevel>();

        public List<Hero> TestEnemies => _testingEnemies.Count > 0 ? _testingEnemies : GetTestEnemies();

        public List<AutoBattlerLevel> TestLevels => _testLevels;
        [SerializeField]
        private List<AutoBattlerLevel> _testLevels = new List<AutoBattlerLevel>();

        private List<Hero> _testingEnemies = new();

        private List<Hero> GetTestEnemies()
        {
            var result = new List<Hero>();

            _testLevels.SelectMany(l => l.Enemies
                .Select(e => e.Hero))
                .ForEach((h, i) => result.Add(h.Clone()));

            _testingEnemies = result;

            return result;
        }
    }
}
