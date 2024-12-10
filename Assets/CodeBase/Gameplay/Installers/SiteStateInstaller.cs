using Extensions;
using Gameplay.Model;
using Gameplay.View;
using Zenject;

namespace Gameplay.Installers
{
    public class SiteStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SiteStateBuildingView>()
                .FromChildComponent<SiteStateBuildingView>(this)
                .AsSingle();
            Container.BindInterfacesTo<BuildingStateHandler<ISiteStateHandler>>()
                .AsSingle()
                .WithArguments(EBuildingState.Site);
        }
    }
}
