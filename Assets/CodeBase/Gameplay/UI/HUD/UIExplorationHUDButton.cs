using UI.Core;
using Cysharp.Threading.Tasks;
using Infrastructure.Scenes;
using UIKit;
using UniRx;
using Zenject;
using UnityEngine;

namespace Gameplay.UI.HUD
{
    public class UIExplorationHUDButton : MarkedButton
    {

    }

    public class UIExplorationHUDButtonPresenter : UIScreenSubPresenter<UIExplorationHUDButton>
    {
        [Inject] private SceneTransitions _sceneTransitions;
        public override async UniTask BeforeShow(CompositeDisposable disposables)
        {
            _view.OnClick
                .Subscribe(_ => _sceneTransitions.LoadExploration(ETransition.Clouds).Forget())
                .AddTo(disposables);

            /*_view.OnClick
                .Subscribe(_ =>
                {
                    _sceneTransitions.LoadExploration(ETransition.Clouds).Forget();
                    GameObject.Destroy(_viewHUD.gameObject);

                }).AddTo(disposables);*/
        }

        public override async UniTask AfterShow(CompositeDisposable disposables)
        {

        }
    }
}