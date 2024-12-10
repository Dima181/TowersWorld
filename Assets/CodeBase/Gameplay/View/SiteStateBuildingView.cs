using Gameplay.Model;
using UnityEngine;

namespace Gameplay.View
{
    public class SiteStateBuildingView : MonoBehaviour, ISiteStateHandler
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
