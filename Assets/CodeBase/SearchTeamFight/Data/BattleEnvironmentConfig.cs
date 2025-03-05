using UnityEngine;

namespace SearchTeamFight.Data
{
    [CreateAssetMenu(fileName = "BattleEnvironment", menuName = "AutoBattlerLevelSystem/BattleEnvironment")]
    public class BattleEnvironmentConfig : ScriptableObject
    {
        public GameObject Environment;
    }
}
