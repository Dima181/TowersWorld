using UI.Core;
using Zenject;

namespace Gameplay.Buildings.UI
{
    public class BuildingsUIInstaller : Installer<BuildingsUIInstaller>
    {
        private const string INFO_POPUP_PATH = "UI/Buildings/Info/Info Popup";

        public override void InstallBindings()
        {
            Container.BindPresenter<UIBuildingInfoPopupPresenter>()
                .WithViewFromPrefab(INFO_POPUP_PATH)
                .AsPopup();
        }
    }
}
