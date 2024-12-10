using Gameplay.Model;
using UnityEngine;

namespace Gameplay.View
{
    public class StayStateBuildingView : MonoBehaviour, IStayStateHandler
    {
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
