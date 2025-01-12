using Cysharp.Threading.Tasks;
using Gameplay.Buildings.Interations;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UI.Core;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    [UsedImplicitly]
    public sealed class PopupPresenter : UIScreenPresenter<PopupView, InteractionArguments>
    {
        private readonly Dictionary<Type, IInteractionConfigurator> _actionConfigurators;
        private IPopupContext _popupContext;

        public PopupPresenter(List<IInteractionConfigurator> actionConfiguretors)
        {
            _actionConfigurators = new();

            foreach (var configurator in actionConfiguretors)
            {
                _actionConfigurators[configurator.OptionType] = configurator;
            }
        }

        protected override async UniTask BeforeShow(InteractionArguments arguments, CompositeDisposable disposables)
        {
            UpdateContext(arguments);
            _view.HideAllButtons();

            foreach (var onClickObservable in _view.AllButtonsClickObservables)
            {
                onClickObservable.Subscribe(_ => HideAndForget()).AddTo(disposables);
            }

            ConfigureButtons(arguments.ActionOptions, disposables);
            _view.OnCloseClicked
                .Subscribe(_ => HideAndForget())
                .AddTo(disposables);

            await _view.Show();
        }

        protected override async UniTask AfterHide()
        {
            await _view.Hide();
        }

        private void UpdateContext(InteractionArguments arguments)
        {
            _popupContext = new PopupContext(
                arguments.Model,
                arguments.View,
                _view);
        }
        
        private void ConfigureButtons(
            IReadOnlyList<IInteractionData> actionOptions, 
            CompositeDisposable disposables)
        {
            foreach (var actionOption in actionOptions)
            {
                if (!_actionConfigurators.TryGetValue(actionOption.GetType(), out var configurator))
                {
                    throw new ArgumentException(
                        $"No instance of {nameof(IInteractionConfigurator)} for {actionOption.GetType().Name}");
                }

                configurator.Configure(actionOption, _popupContext, disposables);
            }
        }
    }
}
