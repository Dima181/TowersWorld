using Infrastructure.Transitions;
using Zenject;

namespace Infrastructure.Scenes
{
    public class ScenesFlowInstaller : Installer<ScenesFlowInstaller>
    {
        private const string CLOUDS_PATH = "Systems/Clouds Transition";
        private const string BLACK_PATH = "Systems/Black Transition";

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .AsSingle();

            Container.Bind<SceneTransitions>()
                .AsSingle();

            Container.Bind<CloudsTransitionView>()
                .FromComponentInNewPrefabResource(CLOUDS_PATH)
                .WithGameObjectName("[CLOUDS_TRANSITION]")
                .AsSingle()
                .OnInstantiated<CloudsTransitionView>((ic, o) =>
                {
                    o.ResetPosition();
                    o.gameObject.SetActive(false);
                });

            Container.Bind<OutBlackScreenTransitionView>()
                .FromComponentInNewPrefabResource(BLACK_PATH)
                .WithGameObjectName("[BLACK_TRANSITION]")
                .AsSingle()
                .OnInstantiated<OutBlackScreenTransitionView>((ic, o) =>
                {
                    o.ResetAlpha();
                    o.gameObject.SetActive(false);
                });

            Container.BindInterfacesAndSelfTo<NoTransition>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CloudsTransition>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<OutBlackScreenTransition>()
                .AsSingle();
        }
    }
}
