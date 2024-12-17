using CodeBase.Core;
using Cysharp.Threading.Tasks;
using Gameplay.Model;
using System;
using UniRx;

namespace Gameplay.Buildings.Services
{
    public interface IBuildingsService
    {
        UniTask<IReactiveDictionary<EResource, int>> GetUpgradeCost(BuildingModel model);
        UniTask<EBuildingUpgradeResult> Upgrade(BuildingModel model);
        UniTask<bool> CanUpgrade(BuildingModel model);
        UniTask<EBuildingUpgradeResult> FastUpgrade(BuildingModel model);
        UniTask<bool> CanFastUpgrade(BuildingModel model);
        UniTask<TimeSpan> GetNextBuildDuration(BuildingModel model);
    }
}
