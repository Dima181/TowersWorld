﻿using Cysharp.Threading.Tasks;
using Infrastructure.Scenes.Transitions;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Scenes
{
    public class SceneTransitions
    {
        private SceneLoader _sceneLoader;
        private Dictionary<ETransition, ITransition> _transitions;

        public SceneTransitions(SceneLoader sceneLoader, List<ITransition> transitions)
        {
            _sceneLoader = sceneLoader;
            _transitions = transitions.ToDictionary(transition => transition.Type);
        }

        public async UniTask LoadGameplay(ETransition transition, bool forceReload = true)
        {
            await _transitions[transition].ApplyTransition(() =>
            _sceneLoader.LoadGameplay(forceReload, sceneActivationDelay: 0.3f));
        }

        public async UniTask LoadExploration(ETransition transition, bool forceReload = true)
        {
            await _transitions[transition].ApplyTransition(() =>
                _sceneLoader.LoadExploration(forceReload, sceneActivationDelay: 0.3f));
        }

        public async UniTask LoadTeamFight(ETransition transition, bool forceReload = true)
        {
            await _transitions[transition].ApplyTransition(() =>
                _sceneLoader.LoadTeamFight(forceReload, sceneActivationDelay: 0.3f));
        }
    }
}
