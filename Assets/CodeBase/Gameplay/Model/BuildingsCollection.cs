using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Model
{
    public class BuildingsCollection : IEnumerable<BuildingModel>
    {
        private readonly IReadOnlyDictionary<EBuilding, BuildingModel> _buildings;

        public BuildingsCollection(IReadOnlyDictionary<EBuilding, BuildingModel> buildings) => 
            _buildings = buildings;

        public BuildingModel Get(EBuilding Id) => 
            _buildings[Id];

        public IEnumerator<BuildingModel> GetEnumerator() => 
            _buildings.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            _buildings.Values.GetEnumerator();
    }
}
