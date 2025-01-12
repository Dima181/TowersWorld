using Gameplay.Model;
using Gameplay.View;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public class PopupContext : IPopupContext
    {
        public BuildingModel BuildingModel => _buildingModel;
        public BuildingView BuildingView => _buildingView;
        public IPopupView PopupView => _popupView;

        private readonly BuildingModel _buildingModel;
        private readonly BuildingView _buildingView;
        private readonly PopupView _popupView;

        public PopupContext(
            BuildingModel buildingModel, 
            BuildingView buildingView, 
            PopupView popupView)
        {
            _buildingModel = buildingModel;
            _buildingView = buildingView;
            _popupView = popupView;
        }
    }
}
