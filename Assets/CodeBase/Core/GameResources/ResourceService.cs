using Core;
using Inventory.Items.ResourceItems;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UniRx;
using Zenject;

namespace Core.GameResources
{

    public class ResourceService : IResources, IProtectedResource
    {
        [Inject] private readonly ResourcesModel _model;

        private ReactiveCommand<Unit> _changedRespurcesCount = new();
        public IObservable<Unit> ChangedRespurcesCount => _changedRespurcesCount;

        private Dictionary<EResource, ReactiveProperty<int>> AllProtectedResources => _model.ProtectedResources;
        private Dictionary<EResource, ReactiveProperty<int>> AllResources => _model.AllResources;


        // Res

        public IReadOnlyReactiveProperty<int> Get(EResource resource) => GetResource(resource, false);

        public int Amount(EResource resource) => GetResource(resource, false).Value;

        public void Add(
            EResource resource,
            int amount,
            bool needSave = true)
        {
            if (AllResources.ContainsKey(resource) == false)
                AllResources.Add(resource, new ReactiveProperty<int>(0));

            AllResources[resource].Value += amount;
            _changedRespurcesCount.Execute(Unit.Default);
        }

        public bool TrySpend(
            EResource resource,
            int amount,
            bool needSave = true)
        {
            if (AllResources.ContainsKey(resource) == false)
            {
                AllResources.Add(resource, new ReactiveProperty<int>(0));
            }

            if (AllResources[resource].Value - amount < 0) // <=
                return false;

            AllResources[resource].Value -= amount;
            _changedRespurcesCount.Execute(Unit.Default);

            if (GetUnprotectedCount(resource) <= 0)
                AllProtectedResources[resource].Value = AllResources[resource].Value;

            return true;
        }

        public void UnpackResourceItem(ResourceItemModel model, int amount)
        {
            var resourceSum = model.ResourceValue * amount;
            Add(model.ResourceType, resourceSum, true);
        }

        // IProtected

        public IReadOnlyReactiveProperty<int> GetProtectedResource(EResource resource)
        {
            if (!resource.IsProtectable())
                throw new Exception($"{resource} - does not belong to the list of 'protected' type!");

            return GetResource(resource, true);
        }

        public bool TrySpendUnprotected(
            EResource resource, 
            int amount, 
            bool needSave = true)
        {
            if (!resource.IsProtectable())
                throw new Exception($"{resource} - does not belong to the list of 'protected' type!");
            
            return amount <= GetUnprotectedCount(resource) && TrySpend(resource, amount, needSave);
        }

        public bool TryProtect(
            EResource resource, 
            int amount, 
            bool needSave = true)
        {
            if (!resource.IsProtectable())
                throw new Exception($"{resource} - does not belong to the list of 'protected' type!");

            if (AllProtectedResources.ContainsKey(resource) == false)
                AllProtectedResources.Add(resource, new ReactiveProperty<int>(0));

            if (amount >= GetUnprotectedCount(resource))
                return false;

            AllProtectedResources[resource].Value += amount;
            _changedRespurcesCount.Execute(Unit.Default);

            return true;
        }

        //

        private int GetUnprotectedCount(EResource resource)
        {
            if (AllResources.ContainsKey(resource) == false)
            {
                AllResources.Add(resource, new ReactiveProperty<int>(0));
            }

            if (AllProtectedResources.ContainsKey(resource) == false)
            {
                AllProtectedResources.Add(resource, new ReactiveProperty<int>(0));
            }

            int unprotected = AllResources[resource].Value - AllProtectedResources[resource].Value;

            return unprotected;
        }


        private IReadOnlyReactiveProperty<int> GetResource(
            EResource resource, 
            bool isProtected)
        {
            if (AllResources.ContainsKey(resource) == false)
                AllResources.Add(resource, new ReactiveProperty<int>(0));
            

            if (AllProtectedResources.ContainsKey(resource) == false)
                AllProtectedResources.Add(resource, new ReactiveProperty<int>(0));


            return isProtected ? AllProtectedResources[resource] : AllResources[resource];
        }
    }
}
