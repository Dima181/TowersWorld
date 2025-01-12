using Extensions;
using Zenject;

namespace Gameplay.Buildings.Interations
{
    public class InteractionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInteractionDataProvider>()
                .FromRootComponent(this)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InteractionsPresenter>()
                .AsSingle();
        }
    }
}