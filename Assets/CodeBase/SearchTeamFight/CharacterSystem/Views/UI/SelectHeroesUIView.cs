using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class SelectHeroesUIView : MonoBehaviour
    {
        [SerializeField] private Button _quickDeployButton;
        [SerializeField] private SelectHeroUIView _selectHeroUIViewPrefab;
        [SerializeField] private GameObject _topPanel;
        [SerializeField] private LayoutGroup _selectHeroUIViewGroup;

        private readonly List<SelectHeroUIView> _selectHeroUIViews = new();

        public SelectHeroUIView GetSelectHeroUIView()
        {
            var selectHeroUiView = Instantiate(_selectHeroUIViewPrefab, _selectHeroUIViewGroup.transform);
            _selectHeroUIViews.Add(selectHeroUiView);

            return selectHeroUiView;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _topPanel.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _topPanel.SetActive(false);
        }

        public void Clear()
        {
            foreach (var selectHeroUiView in _selectHeroUIViews)
            {
                Destroy(selectHeroUiView.gameObject);
            }

            _selectHeroUIViews.Clear();
        }
    }
}
