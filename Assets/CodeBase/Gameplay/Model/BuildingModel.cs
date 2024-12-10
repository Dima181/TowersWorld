using Gameplay.Acceleration;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Gameplay.Model
{
    public class BuildingModel
    {
        public EBuilding Id => _id;
        public string Name => _name;
        public BuildingConfig Config => _config;
        public string Description => _description;
        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<EBuildingState> State => _state;
        public BaseProgressTaskModel BuildProgressTask => _buildProgressTask;

        public int MaxLevel =>
            _config.Levels.Count - 1;

        public IReadOnlyDictionary<int, int> HealthPerLevel => _config.Levels
            .Select((config, index) => (config.Health, index))
            .Skip(1)
            .ToDictionary(pair => pair.index, pair => pair.Health);

        public IReadOnlyReactiveProperty<int> Health =>
            _level.Select(level => level > 0 ? _config.Levels[level - 1].Health : 0).ToReactiveProperty();

        public ReactiveCommand OnBuildingClick { get; }

        public GameObject BuildingPoint { get; set; }


        private readonly EBuilding _id;
        private readonly string _name;
        private readonly string _description;
        private readonly BuildingConfig _config;
        private readonly ReactiveProperty<int> _level;
        private readonly ReactiveProperty<EBuildingState> _state;
        private BaseProgressTaskModel _buildProgressTask;

        public BuildingModel(
            EBuilding id,
            string name,
            string description,
            BuildingConfig config,
            EBuildingState state,
            int currentLevel,
            BaseProgressTaskModel buildProgressTask)
        {
            _id = id;
            _name = name;
            _description = description;
            _config = config;
            _state = new(state);
            _level = new(Mathf.Clamp(currentLevel, 0, MaxLevel));
            _buildProgressTask = buildProgressTask;
        }

    }
}
