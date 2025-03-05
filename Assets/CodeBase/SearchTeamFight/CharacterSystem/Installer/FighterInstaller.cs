using Heroes.Model;
using SearchTeamFight.CharacterSystem.Controllers;
using SearchTeamFight.CharacterSystem.Models;
using SearchTeamFight.CharacterSystem.Services;
using SearchTeamFight.CharacterSystem.StateMachine;
using SearchTeamFight.CharacterSystem.StateMachine.States;
using SearchTeamFight.CharacterSystem.Views.WorldMap;
using Zenject;

namespace SearchTeamFight.CharacterSystem.Views
{
    public class FighterInstaller : MonoInstaller
    {
        public FighterModel FighterModel => _fighterModel;

        private EHero _heroId;
        private TypesTeam _team;
        private bool _isCreep;
        private FighterView _fighterView;
        private HeroesConfig _heroesConfig;
        private FighterModel _fighterModel;

        public void Prepare(
            EHero heroId, 
            TypesTeam team, 
            bool isCreep, 
            FighterView fighterView, 
            HeroesConfig heroesConfig)
        {
            _heroId = heroId;
            _team = team;
            _isCreep = isCreep;
            _fighterView = fighterView;
            _heroesConfig = heroesConfig;
        }

        public override void InstallBindings()
        {
            Container.Bind<EHero>().FromInstance(_heroId).AsSingle();
            Container.Bind<TypesTeam>().FromInstance(_team).AsSingle();
            Container.Bind<bool>().FromInstance(_isCreep).AsSingle();
            Container.Bind<HeroesConfig>().FromInstance(_heroesConfig).AsSingle();

            Container.Bind<FighterModel>().AsSingle();
            Container.Bind<FighterView>().AsSingle();
            Container.Bind<FighterStateMachine>().AsSingle();

            Container.Bind<FighterState>().To<AttackState>().AsSingle();
            Container.Bind<FighterState>().To<DeathState>().AsSingle();
            Container.Bind<FighterState>().To<IdleState>().AsSingle();
            Container.Bind<FighterState>().To<StunnedState>().AsSingle();
            Container.Bind<FighterState>().To<WinState>().AsSingle();

            Container.BindInterfacesTo<FighterController>().AsSingle();

            _fighterModel = Container.Resolve<FighterModel>();
        }
    }
}
