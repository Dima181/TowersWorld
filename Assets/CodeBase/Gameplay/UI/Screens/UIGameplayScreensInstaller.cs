using UI.Core;
using Gameplay.UI.HUD;
using System;
using Zenject;

namespace Gameplay.UI.Screens
{
    public class UIGameplayScreensInstaller : Installer<UIGameplayScreensInstaller>
    {
        public override void InstallBindings()
        {
            BindExploration();
        }

        private void BindExploration()
        {
            Container.BindSubPresenter<UIExplorationHUDButtonPresenter>()
                .ToParent<UIGameplayHUDPresenter>();
        }
    }
}
