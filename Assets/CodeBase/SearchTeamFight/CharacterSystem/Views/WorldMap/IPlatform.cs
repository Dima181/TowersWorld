using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public interface IPlatform
    {
        public TypesTeam Team { get; }
        public Transform Transform { get; }
    }
}
