using Core;
using Core.GameResources;
using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Infrastructure.Pipeline.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buildings.Services
{
    public class LocalBuildingsService : ILocalService<IBuildingsService>, IBuildingsService
    {
        [Inject] private IResources _resources;
        [Inject] private BuildingsCollection _buildings;

        private CompositeDisposable _disposables;

        public async UniTask Initialize(DiContainer di)
        {
            await UniTask.CompletedTask;
            _disposables = new();
            _resources.Add(EResource.Gems, 10);
            _resources.Add(EResource.Iron, 250);
            _resources.Add(EResource.Meat, 100);
            ValidateBuildings();
        }

        public void Dispose() => 
            _disposables?.Dispose();

        public async UniTask<IReadOnlyDictionary<EResource, int>> GetUpgradeCost(BuildingModel model)
        {
            await UniTask.CompletedTask;
            return new Dictionary<EResource, int>()
            {
                { EResource.Iron, model.Config.Levels[model.Level.Value + 1].ResourcesConfig.IronCost },
                { EResource.Meat, model.Config.Levels[model.Level.Value + 1].ResourcesConfig.MeatCost },
                /*{ EResource.Gems, model.Level.Value * 10 },*/
            };
        }

        public async UniTask<EBuildingUpgradeResult> Upgrade(BuildingModel model)
        {
            if (model.IsMaxLevel.Value)
                return EBuildingUpgradeResult.Error;

            if (!_buildings.CheckRequires(model.UpgradeBuildingsRequires))
                return EBuildingUpgradeResult.MissingRequires;

            foreach (var (type, cost) in model.UpgradeCost)
            {
                if (cost > _resources.Amount(type))
                    return EBuildingUpgradeResult.MissingResources;
            }

            List<BuildingModel> buildingsQueue =
                _buildings.Where(buildingModel => buildingModel.State.Value == EBuildingState.Build).ToList();

            if (buildingsQueue.Count >= 2)
                return EBuildingUpgradeResult.ReachedSlotLimit;

            foreach (var (type, cost) in model.UpgradeCost)
                _resources.TrySpend(type, cost);

            var finishBuildingTime = DateTime.UtcNow + model.NextBuildDuration.Value;
            model.SetBuildingTime(DateTime.UtcNow, finishBuildingTime);
            model.SetState(EBuildingState.Build);
            Observable.Timer(finishBuildingTime)
                .Subscribe(_ =>
                {
                    model.SetLevel(model.Level.Value + 1);
                    model.SetState(EBuildingState.Stay);
                    ValidateBuildings();

                    _buildings.Upgraded.Execute(model);
                }).AddTo(_disposables);

            await UniTask.CompletedTask;

            return EBuildingUpgradeResult.Success;
        }
        
        public async UniTask<bool> CanUpgrade(BuildingModel model)
        {
            await UniTask.CompletedTask;
            EBuildingUpgradeResult result = EBuildingUpgradeResult.Success;
            if (model.IsMaxLevel.Value)
                result = EBuildingUpgradeResult.Error;

            if (_resources.Amount(EResource.Iron) < model.Config.Levels[model.Level.Value + 1].ResourcesConfig.IronCost ||
                _resources.Amount(EResource.Meat) < model.Config.Levels[model.Level.Value + 1].ResourcesConfig.MeatCost)
                result = EBuildingUpgradeResult.MissingResources;

            if(!_buildings.CheckRequires(model.UpgradeBuildingsRequires))
                result = EBuildingUpgradeResult.MissingRequires;

            await UniTask.CompletedTask;

            Debug.Log($"EBuildingUpgradeResult: {result}");
            Debug.Log($"{_resources.Amount(EResource.Iron)}, {_resources.Amount(EResource.Meat)}, {_resources.Amount(EResource.Gems)}");
            return result == EBuildingUpgradeResult.Success;
        }
        
        public async UniTask<TimeSpan> GetNextBuildDuration(BuildingModel model)
        {
            await UniTask.CompletedTask;
            return model.NextBuildDuration.Value;
        }
        
        public async UniTask<EBuildingUpgradeResult> FastUpgrade(BuildingModel model)
        {
            if (model.IsMaxLevel.Value)
                return EBuildingUpgradeResult.Error;

            if (!_buildings.CheckRequires(model.UpgradeBuildingsRequires))
                return EBuildingUpgradeResult.MissingResources;

            if (_resources.Amount(EResource.Gems) < model.FastUpgradeCost)
                return EBuildingUpgradeResult.MissingRequires;

            _resources.TrySpend(EResource.Gems, model.FastUpgradeCost);

            model.SetLevel(model.Level.Value + 1);
            ValidateBuildings();

            await UniTask.CompletedTask;
            return EBuildingUpgradeResult.Success;
        }

        private void ValidateBuildings()
        {
            bool changet = false;
            foreach (var building in _buildings)
            {
                if(building.State.Value == EBuildingState.Locked &&
                    _buildings.CheckRequires(building.UpgradeBuildingsRequires))
                {
                    building.SetLevel(building.InitialLevel);
                    building.SetState(building.InitialState);
                    changet = true;
                }
            }

            if (changet)
                ValidateBuildings();
        }

        public async UniTask<bool> CanFastUpgrade(BuildingModel model)
        {
            EBuildingUpgradeResult result = EBuildingUpgradeResult.Success;

            if (model.IsMaxLevel.Value)
                result = EBuildingUpgradeResult.Error;

            if (_resources.Amount(EResource.Gems) < model.FastUpgradeCost)
                result = EBuildingUpgradeResult.MissingResources;

            if (!_buildings.CheckRequires(model.UpgradeBuildingsRequires))
                result = EBuildingUpgradeResult.MissingRequires;

            await UniTask.CompletedTask;

            return result == EBuildingUpgradeResult.Success;
        }
    }
}
