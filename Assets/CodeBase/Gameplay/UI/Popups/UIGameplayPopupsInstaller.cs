using UI.Core;
using Zenject;

namespace Gameplay.UI.Popups
{
    public class UIGameplayPopupsInstaller : Installer<UIGameplayPopupsInstaller>
    {
        private readonly string BUILDING_INTERACTIONS_POPUP_PATH = UIConstants.Popups("Building Interactions Popup");

        public override void InstallBindings()
        {
            Container.BindPresenter<BuildingInteractionsPopup.PopupPresenter>()
                .WithViewFromPrefab(BUILDING_INTERACTIONS_POPUP_PATH)
                .AsPopup();
        }
    }
}
