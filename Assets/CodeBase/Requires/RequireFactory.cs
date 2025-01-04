using Core;
using Core.GameResources;
using Gameplay.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Requires
{
    public class RequireFactory
    {
        [Inject] private IResources _resources;
        [Inject] private BuildingsCollection _buildings;

        public RequireBuilder New() => new RequireBuilder(this);

        public IEnumerable<IRequire> Create(IReadOnlyDictionary<EBuilding, int> buildingRequires) =>
            buildingRequires.Select(pair => new BuildingRequire(_buildings, pair.Key, pair.Value));

        public IEnumerable<IRequire> Create(IReadOnlyDictionary<EResource, int> resourceRequires) => 
            resourceRequires.Select(pair => new ResourceRequire(_resources, pair.Key, pair.Value));
    }
}