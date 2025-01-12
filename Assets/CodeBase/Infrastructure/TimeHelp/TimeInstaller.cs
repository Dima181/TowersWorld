using Zenject;

namespace Infrastructure.TimeHelp
{
    public class TimeInstaller : Installer<TimeInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TimeHelper>()
                .AsSingle();
        }
    }
}
