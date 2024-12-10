using Gameplay.Acceleration;
using Gameplay.Model;
using UnityEngine;

namespace Gameplay.View
{
    public class BuildStateBuildingView : MonoBehaviour, IBuildStateHandler
    {
        public BaseProgressTaskView BuildProgressView => _buildProgressView;
        [SerializeField] private BaseProgressTaskView _buildProgressView;

        public void OnEnterState()
        {
            gameObject.SetActive(true);
        }

        public void OnExitState()
        {
            gameObject.SetActive(false);
        }
    }
}
