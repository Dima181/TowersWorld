using System;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public interface IPopupView
    {
        public IObservable<Unit> OnCloseClicked { get; }
        IObservable<Unit> GetButtonObservable(EButtonId buttonId);
        void ShowButton(EButtonId buttonId);
        void HideButton(EButtonId buttonId);
    }
}
