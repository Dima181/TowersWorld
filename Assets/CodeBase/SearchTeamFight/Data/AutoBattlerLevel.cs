using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SearchTeamFight.Data
{

    [CreateAssetMenu(fileName = "Level", menuName = "AutoBattlerLevelSystem/Level")]
    public class AutoBattlerLevel : ScriptableObject
    {
        public BattleEnvironmentConfig EnvironmentConfig;

        /*public InventoryRewardContainer RewardsContainer;*/

        public List<EnemySlot> Enemies = new List<EnemySlot>();
    }
}
