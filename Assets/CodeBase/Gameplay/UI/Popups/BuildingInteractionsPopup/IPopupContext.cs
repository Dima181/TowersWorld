using Gameplay.Model;
using Gameplay.View;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public interface IPopupContext
    {
        BuildingModel BuildingModel { get; }
        BuildingView BuildingView { get; }
        IPopupView PopupView { get; }
    }
}
