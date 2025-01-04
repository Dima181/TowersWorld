using Core;
using Cysharp.Threading.Tasks;
using Gameplay.Model;
using System.Collections.Generic;
using UI.Core;
using UniRx;

namespace Gameplay.Buildings.UI
{
    public class UIBuildingInfoPopupPresenter : UIScreenPresenter<UIBuildingInfoPopupView, BuildingModel>
    {
        private readonly List<UILevelHealthItem> _createdItems = new();

        protected override UniTask AfterShow(BuildingModel model, CompositeDisposable disposables) =>
            UniTask.CompletedTask;

        protected override UniTask BeforeShow(BuildingModel model, CompositeDisposable disposables)
        {
            _view.OnCloseClick
                .Subscribe(_ => HideAndForget())
                .AddTo(disposables);

            _view.SetDescription(model.Description);

            _createdItems.DestroyAndClear();

            foreach (var item in model.HealthPerLevel)
            {
                var forceItem = _view.CreateItem();
                _createdItems.Add(forceItem);
                forceItem.Construct(item.Key, item.Value);
                if (item.Key == model.Level.Value)
                    forceItem.Active(true);
            }

            return Complited;
        }
    }
}
