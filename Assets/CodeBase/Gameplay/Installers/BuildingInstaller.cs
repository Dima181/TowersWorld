using Gameplay.Model;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class BuildingInstaller : MonoInstaller
    {
        public EBuilding Id => _id;
        [SerializeField] private EBuilding _id;

        public override void InstallBindings()
        {
            MonoInstaller[] installers = gameObject.GetComponents<MonoInstaller>();

            foreach (var installer in installers)
            {
                if (installer == this || installer == null)
                    continue;

                Debug.Log(installer.ToString());
                Container.Inject(installer);
                installer.InstallBindings();
            }
        }
        
        /*private void Start()
        {
            MonoInstaller[] installers = gameObject.GetComponents<MonoInstaller>();

            foreach (var installer in installers)
            {
                if (installer == this || installer == null)
                    continue;

                Debug.Log(installer.ToString());
                Container.Inject(installer);
                installer.InstallBindings();
            }
        }*/
    }
}
