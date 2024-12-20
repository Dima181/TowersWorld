﻿using CodeBase.UI.Core;
using Core.GameResources;
using Currencies;
using Cysharp.Threading.Tasks;
using Gameplay.Buildings.Services;
using Gameplay.Model;
using Infrastructure.TimeHelper;
using Requires;
using Requires.UI;
using UI;
using UniRx;
using UnityEngine.Assertions;
using Zenject;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingUpgradePopupPresenter : UIScreenPresenter<UIBuildingUpgradePopupView, BuildingModel>
    {
        [Inject] private readonly UINavigator _uiNavigator;
        [Inject] private IBuildingsService _buildingsService;
        /*[Inject] private IResources _resources;*/
        [Inject] private TimeHelper _timeHelper;
        [Inject] private RequireFactory _requireFactory;
        [Inject] private UIRequireFactory _uiRequireFactory;

        protected override async UniTask BeforeShow(BuildingModel model, CompositeDisposable disposables)
        {
            Assert.IsFalse(model.IsMaxLevel.Value, "Building Is Max Level");

            var buildDuration = model.NextBuildDuration.Value;
            var amount = buildDuration.GetSkipHardCost();

            /*await _view
                .*/
        }
    }
}
