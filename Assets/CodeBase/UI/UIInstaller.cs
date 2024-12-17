using CodeBase.UI.Core;
using UI.Core;
using Zenject;

namespace UI
{
    public abstract class UIInstaller<THudView, THudPresenter> : MonoInstaller
        where THudPresenter : UIScreenPresenter<THudView>, ISimpleUIScreenPresenter
        where THudView : UIScreenView
    {
        protected abstract string HudScreenPath { get; }

        public override void InstallBindings()
        {
            Container.Bind<UIRoot>()
                .FromComponentInNewPrefabResource("Systems/UI Root")
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UINavigator>()
                .AsSingle();

            Container.BindInitializableExecutionOrder<UINavigator>(-200);

            Container.Bind<THudView>()
                .FromComponentInNewPrefabResource(HudScreenPath)
                .WithGameObjectName("HUD")
                .UnderTransform(ic => ic.Container.Resolve<UIRoot>().Screens.transform)
                .AsSingle()
                .OnInstantiated<THudView>((ic, o) => o.gameObject.SetActive(false));

            Container.Bind<THudPresenter>()
                .AsSingle()
                .WithArgumentsExplicit(new[] { new TypeValuePair(typeof(EScreenType), EScreenType.Screen) });

            Container.Bind<ISimpleUIScreenPresenter>()
                .WithId("HUD")
                .FromResolveGetter<THudPresenter>(t => t)
                .AsSingle();


            InstallBindingsInternal();
        }

        protected abstract void InstallBindingsInternal();
    }
}
