using UI.Core;
using Zenject;

namespace Gameplay.Buildings.UI
{
    public class BuildingsUIInstaller : Installer<BuildingsUIInstaller>
    {
        private const string UPGRADE_POPUP_PATH = "UI/Buildings/Upgrade/Upgrade Popup";
        private const string INFO_POPUP_PATH = "UI/Buildings/Info/Info Popup";

        public override void InstallBindings()
        {
            Container.BindPresenter<UIBuildingInfoPopupPresenter>()
                .WithViewFromPrefab(INFO_POPUP_PATH)
                .AsPopup();

            Container.BindPresenter<UIBuildingUpgradePopupPresenter>()
                .WithViewFromPrefab(UPGRADE_POPUP_PATH)
                .AsPopup();
        }
    }
}
