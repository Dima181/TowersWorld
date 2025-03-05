using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class FightDefeatUI : MonoBehaviour
    {
        [SerializeField] private Button _reloadLevelButton;
        [SerializeField] private Button _returnHomeButton;
        [SerializeField] private Button _returnHomeButton2;

        [Inject] private SceneSwither _sceneSwither;

        private void Start()
        {
            /*_reloadLevelButton.onClick.AddListener(SceneSwither.ReloadLevel);
            _returnHomeButton.onClick.AddListener(SceneSwither.ReturnHome);
            _returnHomeButton2.onClick.AddListener(SceneSwither.ReturnHome);*/
        }
    }
}
