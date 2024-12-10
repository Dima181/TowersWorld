using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Model
{
    public class BuildingLevelConfig
    {
        public int Health { get; private set; }
        public float ShootInterval { get; private set; }
        public float Range { get; private set; }
        public GameObject ProjectilePrefab { get; private set; }
        public float LastShotTime { get; private set; }
        public TimeSpan BuildDuration { get; private set; }
        public IReadOnlyDictionary<EBuilding, int> BuildingsRequires { get; private set; }

        public BuildingLevelConfig(
            int health,
            float shootInterval,
            float range,
            GameObject projectilePrefab,
            float lastShotTime,

            TimeSpan buildDuration,
            IReadOnlyDictionary<EBuilding, int> buildingsRequires)
        {
            Health = health;
            ShootInterval = shootInterval;
            Range = range;
            ProjectilePrefab = projectilePrefab;
            LastShotTime = lastShotTime;

            BuildDuration = buildDuration;
            BuildingsRequires = buildingsRequires;
        }
    }
}
