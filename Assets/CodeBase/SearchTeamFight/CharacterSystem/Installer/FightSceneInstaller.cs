using SearchTeamFight.CharacterSystem.Views.UI;
using SearchTeamFight.CharacterSystem.Configs;
using UnityEngine;
using Zenject;
using SearchTeamFight.CharacterSystem.HeaderFightUI;

namespace SearchTeamFight.CharacterSystem.Views
{
    public class FightSceneInstaller : MonoInstaller
    {
        [SerializeField] private FightConfig _fightConfig;

        [SerializeField] private FightUIView _fightUIView;
        [SerializeField] private HeroesUIView _heroesUIView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private SelectHeroesUIView _selectHeroesUIView;
        [SerializeField] private HeaderFightView _headerFightView;

        [SerializeField] private FightDefeatUI _fightDefeatUI;
        /*[SerializeField] private FightRewardUI _fightRewardUI;*/
    }
}
