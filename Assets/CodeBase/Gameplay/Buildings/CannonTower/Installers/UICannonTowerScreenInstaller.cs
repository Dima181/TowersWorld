using UI.Core;
using Gameplay.Buildings.CannonTower.UI;
using Zenject;

namespace Gameplay.Buildings.CannonTower.Installers
{
    public class UICannonTowerScreenInstaller : Installer<UICannonTowerScreenInstaller>
    {
        private readonly string CANNON_TOWER_SCREEN_PATH = UIConstants.Buildings(@"Arena/Arena Screen");

        public override void InstallBindings()
        {
            Container.BindPresenter<UICannonTowerScreenPresenter>()
                .WithViewFromPrefab(CANNON_TOWER_SCREEN_PATH)
                .AsScreen();
        }
    }
}
