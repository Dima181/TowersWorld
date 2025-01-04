using Core.GameResources;
using System;
using Zenject;

namespace Core
{
    public class BootAllSceneInstaller : Installer<BootAllSceneInstaller>
    {
        public override void InstallBindings()
        {
            Install_Services();
        }

        private void Install_Services()
        {
            Container
                .Bind<ResourcesModel>()
                .AsSingle();

            // Resources
            Container
                .BindInterfacesAndSelfTo<ResourceService>()
                .AsSingle();
        }
    }
}
