using TMPro;
using UI.Core;
using UI.UI_Animation_scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingUpgradePopupView : UIScreenView
    {
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
    }
}
