using Cysharp.Threading.Tasks;
using Infrastructure.Scenes.Transitions;
using Infrastructure.Scenes;
using System;
using Zenject;

namespace Infrastructure.Transitions
{
    public class CloudsTransition : ITransition
    {
        public ETransition Type => ETransition.Clouds;

        [Inject] private CloudsTransitionView _view;

        public async UniTask ApplyTransition(Func<UniTask> func)
        {
            await _view.SetActive();
            await func();
            await _view.SetInactive();
        }
    }
}