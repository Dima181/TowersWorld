using System;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class EnemyPlatform : MonoBehaviour, IPlatform
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _occupied = false;

        public TypesTeam Team => TypesTeam.Enemy;

        public Transform Transform => transform;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void Start()
        {
            _occupied = CheckOccupation();
        }

        private bool CheckOccupation()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("EnemiesMob"))
                    return true;
            }
            return false;
        }
    }
}
