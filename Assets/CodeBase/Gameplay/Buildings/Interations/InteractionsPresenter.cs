using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Gameplay.UI.Popups.BuildingInteractionsPopup;
using Gameplay.View;
using JetBrains.Annotations;
using System;
using UniRx;
using UnityEngine.UIElements;

namespace Gameplay.Buildings.Interations
{
    [UsedImplicitly]
    public sealed class InteractionsPresenter : IBuildingInitializable, IBuildingDisposable
    {
        private readonly BuildingModel _model;
        private readonly BuildingView _view;
        private readonly IInteractionDataProvider _optionsProvider;
        private readonly PopupPresenter _popupPresenter;
        /*private readonly ICameraController _cameraController;*/

        private CompositeDisposable _disposable;

        public InteractionsPresenter(
            BuildingModel model, 
            BuildingView view, 
            IInteractionDataProvider optionsProvider, 
            PopupPresenter popupPresenter)
        {
            _model = model;
            _view = view;
            _optionsProvider = optionsProvider;
            _popupPresenter = popupPresenter;
        }

        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _view.OnClick.Subscribe(_ => OnClick()).AddTo(_disposable);
        }

        public void Dispose() => 
            _disposable?.Dispose();

        private void OnClick()
        {
            var arguments = new InteractionArguments(
                _model,
                _view,
                _optionsProvider.InteractionData);

            _popupPresenter.Show(arguments).Forget();
        }
    }
}