using Gameplay.Buildings.Interations.Implementations;
using Gameplay.Buildings.UI;
using Gameplay.Model;
using JetBrains.Annotations;
using UI;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup.InteractionConfigurator
{
    [UsedImplicitly]
    public class ShowBuildingInfoWindowConfigurator : AbstractInteractionConfigurator<ShowBuildingInfoWindowData>
    {
        private readonly UINavigator _uiNavigation;
        private const EButtonId BUTTON_CODE = EButtonId.DETAILS_BUTTON;

        public ShowBuildingInfoWindowConfigurator(UINavigator uiNavigation)
        {
            _uiNavigation = uiNavigation;
        }

        protected override void ConfigureInternal(
            ShowBuildingInfoWindowData data,
            IPopupContext context,
            CompositeDisposable disposables)
        {
            context.PopupView.GetButtonObservable(BUTTON_CODE).Subscribe(_ =>
            {
                _uiNavigation.Show<UIBuildingInfoPopupPresenter, BuildingModel>(context.BuildingModel);
            }).AddTo(disposables);
            context.PopupView.ShowButton(BUTTON_CODE);
        }
    }
}
