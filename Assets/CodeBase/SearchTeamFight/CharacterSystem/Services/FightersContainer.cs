using NUnit.Framework;
using SearchTeamFight.CharacterSystem.Models;
using SearchTeamFight.CharacterSystem.StateMachine;
using SearchTeamFight.CharacterSystem.Views.WorldMap;
using SearchTeamFight.Data;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace SearchTeamFight.CharacterSystem.Services
{
    public class FightersContainer
    {
        public static Subject<(FighterModel, IPlatform)> FighterConnected;
        public static Subject<(FighterModel, IPlatform)> FighterDisconnected;

        public ReactiveCommand<FighterModel> OnFighterAdded = new();
        public ReactiveCommand<FighterModel> OnFighterRemoved = new();

        private List<FighterModel> _fighterModels = new();
        private List<FighterView> _fighterViews = new();

        private Dictionary<FighterModel, FighterView> _fighterViewByModel = new();
        private Dictionary<FighterView, FighterModel> _fighterModelByView = new();

        private Dictionary<FighterModel, FighterStateMachine> _fighterStateMachineByModel = new();

        [Inject] private PlayersFightersDataScript _charPositionsData;
        [Inject] private PlacePlatformsView _placePlatformsView;

        private Dictionary<TypesTeam, List<FighterModel>> _fighterModelsByTeam = new()
        {
            { TypesTeam.Player, new() },
            { TypesTeam.Enemy, new() }
        };

        private Dictionary<TypesTeam, List<FighterView>> _fighterViewsByTeam = new()
        {
            { TypesTeam.Player, new() },
            { TypesTeam.Enemy, new() }
        };

        private Dictionary<IPlatform, FighterView> _fighterViewsByPlatform = new();
        private Dictionary<FighterView, IPlatform> _platformsByFighterView = new();

        public IReadOnlyList<FighterModel> FighterModels => _fighterModels;
        public IReadOnlyList<FighterView> FighterViews => _fighterViews;

        public IReadOnlyDictionary<FighterModel, FighterView> FighterViewByModel => _fighterViewByModel;
        public IReadOnlyDictionary<FighterView, FighterModel> FighterModelByView => _fighterModelByView;

        public IReadOnlyDictionary<FighterModel, FighterStateMachine> FighterStateMachineByModel =>
            _fighterStateMachineByModel;

        public IReadOnlyDictionary<IPlatform, FighterView> FighterViewsByPlatform => _fighterViewsByPlatform;
        public IReadOnlyDictionary<FighterView, IPlatform> PlatformsByFighterView => _platformsByFighterView;

        public void AddFighter(
            FighterModel fighterModel, 
            FighterView fighterView, 
            FighterStateMachine fighterStateMachine)
        {
            _fighterModels.Add(fighterModel);
            _fighterViews.Add(fighterView);

            _fighterViewByModel.Add(fighterModel, fighterView);
            _fighterModelByView.Add(fighterView, fighterModel);

            _fighterModelsByTeam[fighterModel.Team].Add(fighterModel);
            _fighterViewsByTeam[fighterModel.Team].Add(fighterView);

            _fighterStateMachineByModel.Add(fighterModel, fighterStateMachine);

            OnFighterAdded.Execute(fighterModel);
        }

        public void RemoveFighter(
            FighterModel fighterModel, 
            FighterView fighterView,
            FighterStateMachine fighterStateMachine)
        {
            _fighterModels.Remove(fighterModel);
            _fighterViews.Remove(fighterView);

            _fighterViewByModel.Remove(fighterModel);
            _fighterModelByView.Remove(fighterView);

            _fighterModelsByTeam[fighterModel.Team].Remove(fighterModel);
            _fighterViewsByTeam[fighterModel.Team].Remove(fighterView);

            _fighterStateMachineByModel.Remove(fighterModel);

            OnFighterRemoved.Execute(fighterModel);
        }

        public IEnumerable<FighterModel> GetFighterModelByTeam(TypesTeam team) =>
            _fighterModelsByTeam[team];

        public IEnumerable<FighterView> GetFighterViewByTeam(TypesTeam team) =>
            _fighterViewsByTeam[team];
    }
}
