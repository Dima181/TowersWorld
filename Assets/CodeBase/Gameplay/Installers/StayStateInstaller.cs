using Extensions;
using Gameplay.Model;
using Gameplay.View;
using Zenject;

namespace Gameplay.Installers
{
    public class StayStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StayStateBuildingView>()
                .FromChildComponent<StayStateBuildingView>(this)
                .AsSingle();

            Container.BindInterfacesTo<BuildingStateHandler<IStayStateHandler>>()
                .AsSingle()
                .WithArguments(EBuildingState.Stay);
        }
    }
}
