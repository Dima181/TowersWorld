using System;
using Infrastructure.Pipeline.DataProviders;
using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;

namespace Inventory
{
    public class InventoryDataProvider : LocalDataProvider<InventoryModel>
    {
        [Inject] private DiContainer _di;

        protected override async UniTask<InventoryModel> Load(DiContainer di, DisposableManager disposableManager)
        {
            var database = Resources.Load<InventoryItems>("Inventory Database");
            /*database.Initialize();*/
            await UniTask.CompletedTask;
            return _di.Instantiate<InventoryModel>(new object[] { database });
        }
    }
}
