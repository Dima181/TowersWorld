using Infrastructure.View;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>()
                .AsSingle();

            Container.Bind<BootstrapView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}