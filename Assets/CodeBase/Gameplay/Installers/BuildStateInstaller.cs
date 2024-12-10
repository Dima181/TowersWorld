using Extensions;
using Gameplay.Model;
using Gameplay.View;
using Zenject;

namespace Gameplay.Installers
{
    public class BuildStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildStateBuildingView>()
                .FromChildComponent<BuildStateBuildingView>(this)
                .AsSingle();
            Container.BindInterfacesTo<BuildingStateHandler<IBuildStateHandler>>()
                .AsSingle()
                .WithArguments(EBuildingState.Build);
        }
    }
}
