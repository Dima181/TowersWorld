using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Model
{
    public class BuildingLevelConfig
    {
        public BuildingStateConfig StateConfig { get; }
        public BuildingResourcesConfig ResourcesConfig { get; }

        public int GemsCost { get; }


        public TimeSpan BuildDuration { get; }
        public IReadOnlyDictionary<EBuilding, int> BuildingsRequires { get; }

        public BuildingLevelConfig(
            BuildingStateConfig stateConfig,
            BuildingResourcesConfig resourcesConfig,

            int gemsCost,

            TimeSpan buildDuration,
            IReadOnlyDictionary<EBuilding, int> buildingsRequires)
        {
            StateConfig = stateConfig;
            ResourcesConfig = resourcesConfig;

            GemsCost = gemsCost;

            BuildDuration = buildDuration;
            BuildingsRequires = buildingsRequires;
        }
    }

    public class BuildingStateConfig
    {
        public int Health { get; }
        public float ShootInterval { get; }
        public float Range { get; }
        public GameObject ProjectilePrefab { get; }
        public float LastShotTime { get; }

        public BuildingStateConfig(
            int health, 
            float shootInterval, 
            float range, 
            GameObject projectilePrefab, 
            float lastShotTime)
        {
            Health = health;
            ShootInterval = shootInterval;
            Range = range;
            ProjectilePrefab = projectilePrefab;
            LastShotTime = lastShotTime;
        }
    }

    public class BuildingResourcesConfig
    {
        public int IronCost { get; }
        public int MeatCost { get; }
        
        public BuildingResourcesConfig(int ironCost, int meatCost)
        {
            IronCost = ironCost;
            MeatCost = meatCost;
        }
    }
}
