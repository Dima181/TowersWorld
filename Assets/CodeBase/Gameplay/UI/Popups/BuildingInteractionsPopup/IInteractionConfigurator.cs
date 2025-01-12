using Gameplay.Buildings.Interations;
using System;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public interface IInteractionConfigurator
    {
        Type OptionType { get; }

        void Configure(IInteractionData data, IPopupContext context, CompositeDisposable disposables);
    }
}
