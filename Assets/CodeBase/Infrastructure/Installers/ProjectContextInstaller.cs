using Infrastructure.Scenes;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}