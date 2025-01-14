using UI.Core;
using Currencies;
using Cysharp.Threading.Tasks;
using Gameplay.Buildings.Services;
using Gameplay.Model;
using Infrastructure.TimeHelp;
using Requires;
using Requires.UI;
using UI;
using UniRx;
using UnityEngine.Assertions;
using Zenject;
using System;
using UnityEngine;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingUpgradePopupPresenter : UIScreenPresenter<UIBuildingUpgradePopupView, BuildingModel>
    {
        [Inject] private readonly UINavigator _uiNavigator;
        [Inject] private IBuildingsService _buildingsService;
        [Inject] private TimeHelper _timeHelper;
        [Inject] private RequireFactory _requireFactory;
        [Inject] private UIRequireFactory _uiRequireFactory;

        protected override async UniTask BeforeShow(BuildingModel model, CompositeDisposable disposables)
        {
            Assert.IsFalse(model.IsMaxLevel.Value, "Building Is Max Level");

            var buildDuration = model.NextBuildDuration.Value;
            var amount = buildDuration.GetSkipHardCost();

            await _view
                .SetName(model.Name)
                .SetLevel(model.Level.Value)
                .SetCanUpgrade(await _buildingsService.CanUpgrade(model))
                .SetUpgradeDuration(_timeHelper.TimeSpanToString(buildDuration))
                .SetCanFastUpgrade(await _buildingsService.CanFastUpgrade(model))
                .SetFastUpgradeCost(model.FastUpgradeCost)
                .Show();

            var requires =
                _requireFactory.New()
                .WithBuildings(model.UpgradeBuildingsRequires)
                .WithResources(model.UpgradeCost);

            /*requires.Ready
                .Subscribe(async ready =>
                {
                    _view.SetCanUpgrade(await _buildingsService.CanUpgrade(model));
                    _view.SetCanFastUpgrade(await _buildingsService.CanFastUpgrade(model));
                });*/

            _view.OnCloseClicked
                .Subscribe(_ => HideAndForget())
                .AddTo(disposables);

            _view.OnUpgradeClicked
                .Subscribe(_ => Upgrade(model))
                .AddTo(disposables);

            _view.OnFastUpgradeClicked
                .Subscribe(_ => FastUpgrade(model))
                .AddTo(disposables);

            _uiRequireFactory
                .Create(requires, _view.RequiresContainer)
                .AddTo(disposables);

        }

        private async void Upgrade(BuildingModel model)
        {
            var buildingResult = await _buildingsService.Upgrade(model);
            switch (buildingResult)
            {
                case EBuildingUpgradeResult.Success:
                    StartTimer(model.NextBuildDuration.Value);
                    HideAndForget();
                    break;

                case EBuildingUpgradeResult.ReachedSlotLimit:
                    HideAndForget();
                    Debug.Log("Доделать <BuildingQueuePresenter>");
                    /*_uiNavigator.Perform<BuildingQueuePresenter>(p => p.ShowAndForget());*/
                    break;
            }
        }


        private async void FastUpgrade(BuildingModel model)
        {
            var buildingResult = await _buildingsService.FastUpgrade(model);
            switch (buildingResult)
            {
                case EBuildingUpgradeResult.Success:
                    HideAndForget();
                    break;

                case EBuildingUpgradeResult.ReachedSlotLimit:
                    HideAndForget();
                    Debug.Log("Доделать <BuildingQueuePresenter>");
                    /*_uiNavigator.Perform<BuildingQueuePresenter>(p => p.ShowAndForget());*/
                    break;
            }

            HideAndForget();
        }
        
        private void StartTimer(TimeSpan duration)
        {
            var finishTime = DateTime.UtcNow + duration;

            Observable.EveryUpdate()
                .Select(_ => finishTime - DateTime.UtcNow)
                .TakeWhile(remainingTime => remainingTime.TotalSeconds > 0)
                .Subscribe(remainingTime =>
                {
                    _view.SetCanUpgrade(false);
                    _view.SetUpgradeDuration(remainingTime.ToString(@"hh\:mm\:ss"));
                }, () =>
                {
                    HideAndForget();
                }).AddTo(_view);

        }

        protected override async UniTask AfterShow(BuildingModel model, CompositeDisposable disposables) => 
            await UniTask.CompletedTask;
    }
}
