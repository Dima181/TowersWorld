using Core;
using Core.GameResources;
using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Infrastructure.Pipeline.Services;
using System;
using System.Collections.Generic;
using UniRx;
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
            _resources.Add(EResource.Gems, 1000);
            /*ValidateBuildings();*/
        }
        public void Dispose() => 
            _disposables?.Dispose();

        public async UniTask<IReadOnlyDictionary<EResource, int>> GetUpgradeCost(BuildingModel model)
        {
            await UniTask.CompletedTask;
            return new Dictionary<EResource, int>()
            {
                { EResource.Iron, model.Level.Value * 100 },
                { EResource.Meat, model.Level.Value * 100 },
                { EResource.Gems, model.Level.Value * 10 },
            };
        }
        
        public async UniTask<EBuildingUpgradeResult> Upgrade(BuildingModel model)
        {
            if (model.IsMaxLevel.Value)
                return EBuildingUpgradeResult.Error;

            if (!_buildings.CheckRequires(model.UpgradeBuildingRequires))
                return EBuildingUpgradeResult.MissingRequires;

            throw new NotImplementedException();
        }

        public UniTask<bool> CanFastUpgrade(BuildingModel model)
        {
            throw new NotImplementedException();
        }

        public UniTask<bool> CanUpgrade(BuildingModel model)
        {
            throw new NotImplementedException();
        }


        public UniTask<EBuildingUpgradeResult> FastUpgrade(BuildingModel model)
        {
            throw new NotImplementedException();
        }

        public UniTask<TimeSpan> GetNextBuildDuration(BuildingModel model)
        {
            throw new NotImplementedException();
        }



    }
}
