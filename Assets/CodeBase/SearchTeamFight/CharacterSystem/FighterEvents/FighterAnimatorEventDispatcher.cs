using UniRx;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.FighterEvents
{
    public class FighterAnimatorEventDispatcher : MonoBehaviour
    {
        public ReactiveCommand<Vector3> OnAttackPerformed = new();
        public ReactiveCommand<Vector3> OnAttackEnded = new();

        [SerializeField] private Transform _shootPoint;

        public void AttackPerformed()
        {
            var position = _shootPoint != null ? _shootPoint.position : Vector3.zero;
            OnAttackPerformed.Execute(position);
        }

        public void AttackEnded()
        {
            var position = _shootPoint != null ? _shootPoint.position : Vector3.zero;
            OnAttackEnded.Execute(position);
        }
    }
}
