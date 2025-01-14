using Zenject;

namespace Infrastructure.Pipeline.Services
{
    public class ServicesInstaller : Installer<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(x => x.AllInterfaces())
                .To(x => x.AllNonAbstractClasses().DerivingFrom<ILocalService>())
                .AsSingle();
        }
    }
}
