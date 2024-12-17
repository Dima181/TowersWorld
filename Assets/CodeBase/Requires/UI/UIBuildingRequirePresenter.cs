using Gameplay.Buildings.UI;
using Gameplay.Model;
using UI;
using Zenject;

namespace Requires.UI
{
    public class UIBuildingRequirePresenter : UIRequirePresenter<BuildingRequire, UIBuildingRequireView>
    {
        [Inject] private BuildingsCollection _buildings;

        [Inject] private UINavigator _uiNavigator;

        public override void Initialize(BuildingRequire require, UIBuildingRequireView view)
        {
            base.Initialize(require, view);

            var requireBuilding = _buildings.Get(require.BuildingId);

            view.SetBuildingRequire(requireBuilding.Name, require.Level);
        }

        protected override void OnFixClicked(BuildingRequire require)
        {
            var requireBuilding = _buildings.Get(require.BuildingId);
            _uiNavigator.Show<UIBuildingUpgradePopupPresenter, BuildingModel>(requireBuilding);
        }
    }
}
