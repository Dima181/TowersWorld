using System;
using TMPro;
using UnityEngine;

namespace Requires.UI
{
    public class UIBuildingRequireView : UIRequireView
    {
        [Header("Building")]
        [SerializeField] private TextMeshProUGUI _buildingRequireText;
        [SerializeField] private string _buildingLevelRequireTemplate = "{0} {1}";

        public void SetBuildingRequire(string buildingName, int buildingLevel) => 
            _buildingRequireText.text = string.Format(_buildingLevelRequireTemplate, buildingName, buildingLevel);

        protected override void SetRequireTextColor(Color color) => 
            _buildingRequireText.color = color;
    }
}
