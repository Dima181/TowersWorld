using Gameplay.Buildings.CannonTower.UI;
using Gameplay.Model;
using Gameplay.View;
using System;
using UI;
using UniRx;
using Zenject;

namespace Gameplay.Buildings.CannonTower.Installers
{
    public class CannonTowerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CannonTowerPresenter>()
                .AsSingle();
        }
    }

    public class CannonTowerPresenter : IBuildingInitializable, IBuildingDisposable
    {
        [Inject] private readonly UINavigator _uiNAvigator;
        [Inject] private readonly BuildingView _buildingView;

        private IDisposable _disposable;

        public void Initialize()
        {
            /*_disposable = _buildingView.OnClick
                .Subscribe(_ => _uiNAvigator.Perform<UICannonTowerScreenPresenter>(p => p.ShowAndForget()));*/
        }

        public void Dispose() => 
            _disposable?.Dispose();
    }
}
