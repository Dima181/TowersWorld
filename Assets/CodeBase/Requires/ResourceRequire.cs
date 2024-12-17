using CodeBase.Core;
using Core.GameResources;
using System;
using UniRx;
using UnityEngine;

namespace Requires
{
    public class ResourceRequire : IRequire
    {
        public IObservable<bool> Ready => _resources.Get(Resource)
            .Select(amountInStorage => Amount <= amountInStorage) // если требуемое количество ресурса (Amount) <= текущему количеству в хранилище (amountInStorage)
            .DistinctUntilChanged();

        public EResource Resource { get; }
        public int Amount {  get; }

        private readonly IResources _resources;

        public ResourceRequire(IResources resources, EResource resource, int amount)
        {
            _resources = resources;
            Resource = resource;
            Amount = amount;
        }

        public IDisposable Accept(IRequireVisitor visitor, Transform container) => 
            visitor.Visit(this, container);
    }
}