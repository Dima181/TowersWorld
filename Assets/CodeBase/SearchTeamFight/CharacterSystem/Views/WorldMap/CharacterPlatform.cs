using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class CharacterPlatform : MonoBehaviour, IPlatform
    {
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private MeshRenderer _meshRenderer;

        public TypesTeam Team => TypesTeam.Player;

        public Transform Transform => transform;
    }
}
