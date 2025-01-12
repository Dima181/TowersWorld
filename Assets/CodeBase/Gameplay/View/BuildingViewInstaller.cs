using Extensions;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    [RequireComponent(typeof(BuildingView))]
    public class BuildingViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            /*Debug.Log(nameof(BuildingViewInstaller));*/

            Container.Bind<BuildingView>()
                .FromRootComponent(this)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BuildingPresenter>()
                .AsSingle();
        }
    }
}
