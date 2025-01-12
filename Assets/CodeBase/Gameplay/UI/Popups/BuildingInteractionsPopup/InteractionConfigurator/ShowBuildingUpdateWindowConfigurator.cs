using Gameplay.Buildings.Interations.Implementations;
using Gameplay.Buildings.UI;
using Gameplay.Model;
using JetBrains.Annotations;
using System;
using UI;
using UniRx;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup.InteractionConfigurator
{
    [UsedImplicitly]
    public class ShowBuildingUpgradeWindowConfigurator : AbstractInteractionConfigurator<ShowBuildingUpgradeWindowData>
    {
        private readonly UINavigator _uiNavigator;
        private const EButtonId BUTTON_CODE = EButtonId.UPGRADE_BUTTON;

        public ShowBuildingUpgradeWindowConfigurator(UINavigator uiNavigator) => 
            _uiNavigator = uiNavigator;


        protected override void ConfigureInternal(
            ShowBuildingUpgradeWindowData data, 
            IPopupContext context, 
            CompositeDisposable disposables)
        {
            context.PopupView.GetButtonObservable(BUTTON_CODE).Subscribe(_ =>
            {
                _uiNavigator.Show<UIBuildingUpgradePopupPresenter, BuildingModel>(context.BuildingModel);
            }).AddTo(disposables);
            context.PopupView.ShowButton(BUTTON_CODE);
        }
    }
}
