using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class HitBoxView : MonoBehaviour
    {
        public ReactiveCommand<GameObject> OnEnter => _onEnter;
        public ReactiveCommand<GameObject> OnExit => _onExit;
     
        private ReactiveCommand<GameObject> _onEnter = new();
        private ReactiveCommand<GameObject> _onExit = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NavMeshAgent _))
                OnEnter.Execute(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out NavMeshAgent _))
                OnExit.Execute(other.gameObject);
        }
    }
}