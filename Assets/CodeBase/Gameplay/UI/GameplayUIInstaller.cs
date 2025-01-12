using Core;
using Gameplay.Buildings.CannonTower.UI;
using Gameplay.Buildings.UI;
using Gameplay.UI.HUD;
using Gameplay.UI.Popups;
using Gameplay.UI.Popups.BuildingInteractionsPopup;
using Gameplay.UI.Screens;
using System;
using UI;
using UI.Core;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameplayUIInstaller : UIInstaller<UIGameplayHUDView, UIGameplayHUDPresenter>
    {
        protected override string HudPrefabPath => "UI/HUD/Gameplay Hud";

        protected override void InstallBindingsInternal()
        {
            UIGameplayScreensInstaller.Install(Container);
            BuildingsUIInstaller.Install(Container);
            /*BootAllSceneInstaller.Install(Container);*/

            UIGameplayPopupsInstaller.Install(Container);
            InteractionConfiguratorsInstaller.Install(Container);
        }
    }
}
