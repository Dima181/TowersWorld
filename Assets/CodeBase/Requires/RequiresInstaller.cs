using Requires.UI;
using Zenject;

namespace Requires
{
    public class RequiresInstaller : Installer<RequiresInstaller>
    {
        private const string BUILDING_REQUIRE_PATH = "Systems/Building Require UI";
        private const string RESOURCE_REQUIRE_PATH = "Systems/Resource Require UI";

        public override void InstallBindings()
        {
            Container.Bind<UIBuildingRequireView>()
                .FromResource(BUILDING_REQUIRE_PATH)
                .AsCached();

            Container.Bind<UIResourceRequireView>()
                .FromResource(RESOURCE_REQUIRE_PATH)
                .AsCached();

            Container.Bind<RequireFactory>()
                .AsCached();

            Container.Bind<UIRequireFactory>()
                .AsCached();
        }
    }
}
