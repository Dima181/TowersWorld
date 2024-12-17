using Gameplay.Model;
using System;
using UniRx;
using UnityEngine;

namespace Requires
{
    public class BuildingRequire : IRequire
    {
        public IObservable<bool> Ready => _collection.Get(BuildingId).Level
            .Select(level => level >= Level) // (уровень здания больше или равен требуемому другого здания)
            .DistinctUntilChanged();                                            // { EBuilding.Firebox, 1 }

        public EBuilding BuildingId { get; }
        public int Level { get; }

        private readonly BuildingsCollection _collection;

        public BuildingRequire(BuildingsCollection collection, EBuilding buildingId, int level)
        {
            _collection = collection;
            BuildingId = buildingId;
            Level = level;
        }

        public IDisposable Accept(IRequireVisitor visitor, Transform container) =>
            visitor.Visit(this, container);
    }
}