using Gameplay.Buildings.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Requires.UI
{
    public class UIRequireFactory : IRequireVisitor
    {
        [Inject] private DiContainer _di;
        [Inject] private UIBuildingRequireView _buildingRequirePrefab;
        [Inject] private UIResourceRequireView _resourceRequirePrefab;

        public IDisposable Create(IRequire require, Transform container) =>
            require.Accept(this, container);

        public IDisposable Visit(BuildingRequire buildingRequire, Transform container)
        {
            var disposables = new CompositeDisposable();
            var view = _di.InstantiatePrefabForComponent<UIBuildingRequireView>(_buildingRequirePrefab, container);
            var presenter = _di.Instantiate<UIBuildingRequirePresenter>();
            presenter.Initialize(buildingRequire, view);
            disposables.Add(presenter);
            Disposable.Create(() =>
            {
                if (view)
                    Object.Destroy(view.gameObject);
            }).AddTo(disposables);
            return disposables;
        }

        public IDisposable Visit(ResourceRequire resourceRequire, Transform container)
        {
            var disposables = new CompositeDisposable();
            var view = _di.InstantiatePrefabForComponent<UIResourceRequireView>(_resourceRequirePrefab, container);
            var presenter = _di.Instantiate<UIResourceRequirePresenter>();
            presenter.Initialize(resourceRequire, view);
            disposables.Add(presenter);
            Disposable.Create(() =>
            {
                if (view)
                    Object.Destroy(view.gameObject);
            }).AddTo(disposables);
            return disposables;
        }

        public IDisposable Visit(IEnumerable<IRequire> requires, Transform container)
        {
            var disposables = new CompositeDisposable();
            var buildings = requires.Where(r => r is BuildingRequire).OrderBy(r => ((BuildingRequire)r).BuildingId);
            var resources = requires.Where(r => r is ResourceRequire).OrderBy(r => ((ResourceRequire)r).Resource);
            var composites = requires.Where(r => r is CompositeRequire);
            foreach (var r in composites)
                r.Accept(this, container).AddTo(disposables);
            foreach (var r in buildings)
                r.Accept(this, container).AddTo(disposables);
            foreach (var r in resources)
                r.Accept(this, container).AddTo(disposables);
            return disposables;
        }
    }
}
