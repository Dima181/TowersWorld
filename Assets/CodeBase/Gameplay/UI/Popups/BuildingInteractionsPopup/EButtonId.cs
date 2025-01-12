using UnityEngine;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public enum EButtonId : uint
    {
        None = 0,

        [InspectorName("Details Button")]
        DETAILS_BUTTON = 1,
        
        [InspectorName("Upgrage Button")]
        UPGRADE_BUTTON = 2,

        BUTTON_3 = 3,
        BUTTON_4 = 4,

        [InspectorName("Fighter Button")]
        FIGHTERS_BUTTON = 5,
        BUTTON_6 = 6,
        BUTTON_7 = 7,

        [InspectorName("Research Button")]
        RESEARCH_BUTTON = 8,
    }
}
