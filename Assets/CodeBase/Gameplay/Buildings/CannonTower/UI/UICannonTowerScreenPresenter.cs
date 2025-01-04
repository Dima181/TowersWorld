using UI.Core;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Gameplay.Buildings.CannonTower.UI
{
    public class UICannonTowerScreenPresenter : UIScreenPresenter<UICannonTowerScreenView>
    {

        public static bool IsActive;
        protected override UniTask BeforeShow(CompositeDisposable disposables)
        {
            if(IsActive)
            {
                Show().Forget();
                _view.InfoPanel.ShowHide(true);
            }

            _view.OnCloseClick
                .Subscribe(_ => _view.Hide().Forget())
                .AddTo(disposables);

            _view.OnInfoClick
                .Subscribe(_ => _view.InfoPanel.ShowHide(true))
                .AddTo(disposables);

            return Complited;
        }
    }
}
