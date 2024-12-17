using CodeBase.Core;
using Gameplay.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Requires
{
    public class RequireBuilder : IRequire
    {
        private CompositeRequire _require;
        private RequireFactory _factory;

        public IObservable<bool> Ready => _require.Ready;

        public RequireBuilder(RequireFactory factory)
        {
            _require = new();
            _factory = factory;
        }

        public RequireBuilder WithBuildings(IReadOnlyDictionary<EBuilding, int> buildingRequire)
        {
            _require.AddRange(_factory.Create(buildingRequire));
            return this;
        }

        public RequireBuilder WithResources(IReadOnlyDictionary<EResource, int> resourceRequire)
        {
            _require.AddRange(_factory.Create(resourceRequire));
            return this;
        }

        public IDisposable Accept(IRequireVisitor visitor, Transform container) => 
            ((IRequire)_require).Accept(visitor, container);
    }
}