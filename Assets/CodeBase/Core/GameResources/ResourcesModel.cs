using Core;
using System.Collections.Generic;
using UniRx;

namespace Core.GameResources
{
    public class ResourcesModel
    {
        public Dictionary<EResource, ReactiveProperty<int>> AllResources = new();
        public Dictionary<EResource, ReactiveProperty<int>> ProtectedResources = new();

        public ResourcesModel()
        {
            AllResources = new()
            {
                {EResource.Meat, new(0) },
                {EResource.Iron, new(0) }
            };

            ProtectedResources = new()
            {
                {EResource.Meat, new(0) },
                {EResource.Iron, new(0) }
            };
        }
    }
}
