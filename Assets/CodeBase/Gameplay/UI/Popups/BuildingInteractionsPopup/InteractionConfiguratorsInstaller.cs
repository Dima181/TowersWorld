using Gameplay.UI.Popups.BuildingInteractionsPopup.InteractionConfigurator;
using Zenject;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public class InteractionConfiguratorsInstaller : Installer<InteractionConfiguratorsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ShowBuildingUpgradeWindowConfigurator>().AsSingle();
            Container.BindInterfacesTo<ShowBuildingInfoWindowConfigurator>().AsSingle();
        }
    }
}
