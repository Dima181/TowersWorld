using CodeBase.UI.Core;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Requires.UI
{

    public class UIResourceRequireView : UIRequireView
    {
        [Header("Building")]
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TextMeshProUGUI _resourceAmountText;
        [SerializeField] private string _resourceAmountTemplate = "{0}/{1}";

        public void SetResourceRequire(Sprite resourceIcon, int currentAmount, int requiredAmount)
        {
            _resourceIcon.sprite = resourceIcon;
            _resourceAmountText.text = string.Format(_resourceAmountTemplate, 
                currentAmount.ToNiceString(), 
                requiredAmount.ToNiceString());
        }

        protected override void SetRequireTextColor(Color color) => 
            _resourceAmountText.color = color;
    }
}