using Heroes.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SearchTeamFight.Data
{
    public class FightersPositionData : ScriptableObject
    {
        public List<FighterPositionData> Data;
    }

    [Serializable]
    public struct FighterPositionData
    {
        public int PlatformIndex;
        public EHero PrefabIndex;
    }
}
