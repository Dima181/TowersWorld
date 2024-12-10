using UnityEngine;
using Zenject;

namespace Cam
{
    public class Cameras
    {
        public Camera MainCamera => _mainCamera;
        public Camera ScreenCamera => _screenCamera;

        private Camera _mainCamera;
        private Camera _screenCamera;

        public Cameras(Camera mainCamera, Camera screenCamera)
        {
            _mainCamera = mainCamera;
            _screenCamera = screenCamera;
        }
    }

    public class CameraControlInstaller : MonoInstaller<CameraControlInstaller>
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _screenCamera;

        public override void InstallBindings()
        {
            // тут доделать потом
            Debug.Log("CameraControlInstaller: тут доделать потом");

            /*Container
                .BindInterfacesAndSelfTo<CameraController>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container
                .Bind<CameraTouchHandler>()
                .FromComponentsInHierarchy()
                .AsSingle();*/

            Container.Bind<Cameras>()
                .FromMethod(() => new Cameras(_mainCamera, _screenCamera))
                .AsSingle();
        }
    }
}
