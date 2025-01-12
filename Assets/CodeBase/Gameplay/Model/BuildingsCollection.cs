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

        public bool CheckRequires(IReadOnlyDictionary<EBuilding, int> requires)
        {
            foreach (var pair in requires)
                if (_buildings[pair.Key].Level.Value < pair.Value)
                    return false;
            return true;
        }

        public IEnumerator<BuildingModel> GetEnumerator() => 
            _buildings.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            _buildings.Values.GetEnumerator();
    }
}
