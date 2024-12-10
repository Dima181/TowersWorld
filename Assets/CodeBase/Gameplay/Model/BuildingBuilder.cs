using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Model
{
    public class BuildingBuilder
    {
        private EBuilding _id;
        private string _name;
        private List<BuildingLevelConfig> _levels = new();
        private EBuildingState _initialState;
        private int _initialLevel;

        public BuildingBuilder WithId(EBuilding id)
        {
            _id = id;
            return this;
        }

        public BuildingBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BuildingBuilder InitialStay()
        {
            _initialState = EBuildingState.Stay;
            _initialLevel = 1;
            return this;
        }

        public BuildingBuilder InitialSite()
        {
            _initialState = EBuildingState.Site;
            _initialLevel = 0;
            return this;
        }

        public BuildingBuilder AddLevels(
            int[] health,
            float[] shootInterval,
            float[] range,
            GameObject[] projectilePrefab,
            float[] lastShotTime,
            TimeSpan[] buildDurations,
            Dictionary<EBuilding, int>[] upgradeRequires)
        {
            for (int i = 0; i < shootInterval.Length; i++)
            {
                _levels.Add(new BuildingLevelConfig(
                    health[i],
                    shootInterval[i],
                    range[i],
                    projectilePrefab[i],
                    lastShotTime[i],
                    buildDurations[i],
                    upgradeRequires[i]));
            }
            return this;
        }

        public BuildingModel Build()
        {
            var config = new BuildingConfig(_levels, _initialState, _initialLevel);

            return new BuildingModel(
                _id,
                _name,
                "Description",
                config,
                EBuildingState.Stay,
                1,
                null);
        }
    }
}
