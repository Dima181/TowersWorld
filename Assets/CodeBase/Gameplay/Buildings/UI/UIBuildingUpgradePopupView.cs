using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UI;
using UI.Core;
using UI.UI_Animation_scripts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingUpgradePopupView : UIScreenView
    {
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable().Merge(_backgroundCloseButton.OnClickAsObservable());

        public IObservable<Unit> OnUpgradeClicked => _upgradeButton.OnClickAsObservable();

        public IObservable<Unit> OnFastUpgradeClicked => _fastUpgradeButton.OnClickAsObservable();

        public RectTransform RequiresContainer => _requiresContainer;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundCloseButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _fastUpgradeButton;
        [Space]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _nextLevelText;
        [SerializeField] private string _levelFormat;
        [SerializeField] private TextMeshProUGUI _fastCompleteCostText;
        [SerializeField] private TextMeshProUGUI _upgradeDurationText;
        [Space]
        [SerializeField] private RectTransform _requiresContainer;
        [SerializeField] private UIPopupAnimation _animationController;

        public override UniTask Show()
        {
            _animationController.OpenPopup();
            return base.Show();
        }

        public override UniTask Hide()
        {
            _animationController.ClosePopup();
            return base.Hide();
        }

        public UIBuildingUpgradePopupView SetName(string name)
        {
            _nameText.text = name;
            return this;
        }

        public UIBuildingUpgradePopupView SetLevel(int level)
        {
            _levelText.text = string.Format(_levelFormat, level);
            _nextLevelText.text = (level + 1).ToString();
            return this;
        }

        public UIBuildingUpgradePopupView SetUpgradeDuration(string time)
        {
            _upgradeDurationText.text = time;
            return this;
        }

        public UIBuildingUpgradePopupView SetFastUpgradeCost(int cost)
        {
            _fastCompleteCostText.text = cost.ToNiceString();
            return this;
        }

        public UIBuildingUpgradePopupView SetCanFastUpgrade(bool canUpgrade)
        {
            _fastUpgradeButton.interactable = canUpgrade;
            return this;
        }

        public UIBuildingUpgradePopupView SetCanUpgrade(bool canUpgrade)
        {
            _upgradeButton.interactable = canUpgrade;
            return this;
        }
    }
}
