using Infrastructure.Scenes;
using SearchTeamFight.Data;
using UnityEngine;
using Zenject;

namespace SearchTeamFight.CharacterSystem
{
    public class SceneSwither
    {
        [Inject] private LevelIndexHolder _levelIndexHolder;
        [Inject] private AutoBattlerLevels _autoBattlerLevels; 
        [Inject] private SceneTransitions _transitions; 

        public void LoadNextLevel()
        {
            _levelIndexHolder.Index++;
            
            if(_levelIndexHolder.Index == _autoBattlerLevels.Levels.Count)
            {
                _levelIndexHolder.Index = 0;
                Debug.LogWarning("Max Auto Battler Levels happened. Level Index set 0");
            }

            _ = _transitions.LoadTeamFight(ETransition.Clouds);
        }

        public void ReloadLevel() =>
            _ = _transitions.LoadTeamFight(ETransition.Clouds);

        public void ReturnHome() =>
            _ = _transitions.LoadTeamFight(ETransition.Clouds);

        public void LoadExploretion() =>
            _ = _transitions.LoadExploration(ETransition.Clouds);
    }
}
