using Cysharp.Threading.Tasks;
using Gameplay.Installers;
using Gameplay.Model;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class BuildingSpawnPoint : MonoBehaviour
    {
        [SerializeField] private BuildingInstaller _prefab;

        [Inject] private BuildingsCollection _collection;
        [Inject] private DiContainer _di;

        private IDisposable _disposable;

        private void OnEnable()
        {
            var building = _collection.Get(_prefab.Id);

            _disposable = building.State
                .Where(state => state != EBuildingState.Locked)
                .Subscribe(state => Construct(building).Forget());
        }

        private void OnDisable() =>
            _disposable?.Dispose();

        private async UniTaskVoid Construct(BuildingModel building)
        {
            var subContainer = _di.CreateSubContainer();

            subContainer.Bind<BuildingModel>()
                .FromInstance(building)
                .AsSingle();

            Debug.Log(_prefab.Id);

            var installer = subContainer.InstantiatePrefabForComponent<BuildingInstaller>(
                _prefab,
                transform.position,
                transform.rotation,
                transform.parent);

            installer.name = building.Name;
            subContainer.Inject(installer);
            installer.InstallBindings();

            var initializables = subContainer.ResolveAll<IBuildingInitializable>();
            var disposables = subContainer.ResolveAll<IBuildingDisposable>();
            var disposableManager = subContainer.Resolve<DisposableManager>();

            foreach (var initializable in initializables)
                initializable.Initialize();
            foreach (var disposable in disposables)
                disposableManager.Add(disposable);

            await UniTask.NextFrame();
            Destroy(gameObject);
        }
    }
}
