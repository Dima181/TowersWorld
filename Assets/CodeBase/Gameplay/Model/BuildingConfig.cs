using System.Collections.Generic;

namespace Gameplay.Model
{
    public class BuildingConfig
    {
        private readonly List<BuildingLevelConfig> _levels;
        public IReadOnlyList<BuildingLevelConfig> Levels => _levels;
        public EBuildingState InitialState { get; }
        public int InitialLevel { get; }

        public BuildingConfig(
            IReadOnlyList<BuildingLevelConfig> levels,
            EBuildingState initialState,
            int initialLevel)
        {
            _levels = new(levels);
            InitialState = initialState;
            InitialLevel = initialLevel;
        }

        /*public void SetNextLevel(BuildingLevelConfig config, int nextLevel) => _levels[nextLevel] = config;*/
    }
}
