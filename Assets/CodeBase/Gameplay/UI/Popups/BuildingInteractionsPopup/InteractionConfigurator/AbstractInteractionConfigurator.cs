using Gameplay.Buildings.Interations;
using System;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup.InteractionConfigurator
{
    public abstract class AbstractInteractionConfigurator<TActionOption> : IInteractionConfigurator
        where TActionOption : IInteractionData
    {
        public Type OptionType => typeof(TActionOption);

        public void Configure(
            IInteractionData data,
            IPopupContext context,
            CompositeDisposable disposables) 
            => 
                ConfigureInternal((TActionOption)data, context, disposables);

        protected abstract void ConfigureInternal(
            TActionOption data,
            IPopupContext context,
            CompositeDisposable disposables);
    }
}
