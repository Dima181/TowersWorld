using Core.GameResources.Configs;
using Zenject;

namespace Core.GameResources
{
    public class ResourcesProjectInstaller : Installer<ResourcesProjectInstaller>
    {
        private const string CONFIG_PATH = "Resources Config";
        public override void InstallBindings()
        {
            Container.Bind<ResourcesConfig>()
                .FromScriptableObjectResource(CONFIG_PATH)
                .AsSingle();
        }
    }
}
