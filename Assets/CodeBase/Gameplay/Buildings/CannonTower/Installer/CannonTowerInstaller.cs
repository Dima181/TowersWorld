using Gameplay.Buildings.CannonTower.Views;
using Gameplay.Buildings.CannonTower.Views.Description;
using Gameplay.Buildings.CannonTower.Views.DetailedScreen;
using Gameplay.Model;
using UnityEngine;
using Zenject;

namespace Gameplay.Buildings.CannonTower.Installer
{
    public class CannonTowerInstaller : MonoInstaller
    {
        [SerializeField] private CannonTowerInteractionPanelView _cannonTowerInteractionPanel;

        [SerializeField] private UICannonTowerDescription _uiStorageDescription;
        [SerializeField] private UICannonTowerDetailed _uiStorageDetailed;

        [Inject] private readonly BuildingModel _model;

        public override void InstallBindings()
        {
             Container.BindInstance(_cannonTowerInteractionPanel);
        }
    }
}
