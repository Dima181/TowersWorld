using System;
using TMPro;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingInfoPopupView : UIScreenView
    {
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroungButton;
        [SerializeField] private RectTransform _levelPowerContent;
        [SerializeField] private UILevelHealthItem _levelHealthItemPrefab;

        public IObservable<Unit> OnCloseClick => _closeButton.OnClickAsObservable().Merge
            (_backgroungButton.OnClickAsObservable());

        public UILevelHealthItem CreateItem() =>
            Instantiate(_levelHealthItemPrefab, _levelPowerContent);

        public void SetDescription(string description) =>
            _descriptionText.text = description;
    }
}
