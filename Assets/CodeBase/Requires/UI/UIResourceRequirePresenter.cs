using Core.GameResources;
using Core.GameResources.Configs;
using Zenject;
using UniRx;

namespace Requires.UI
{
    public class UIResourceRequirePresenter : UIRequirePresenter<ResourceRequire, UIResourceRequireView>
    {
        [Inject] private IResources _resources;
        [Inject] private ResourcesConfig _config;

        public override void Initialize(ResourceRequire require, UIResourceRequireView view)
        {
            base.Initialize(require, view);
            _resources.Get(require.Resource)
                .Subscribe(count =>
                    view.SetResourceRequire(_config.Get(require.Resource).Icon, count, require.Amount))
                .AddTo(_disposables);
        }
    }
}