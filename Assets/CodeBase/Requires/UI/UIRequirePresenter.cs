using System;
using UniRx;

namespace Requires.UI
{
    public abstract class UIRequirePresenter<T, TView> : IDisposable
        where T : IRequire
        where TView : UIRequireView
    {
        protected CompositeDisposable _disposables;

        public virtual void Initialize(T require, TView view)
        {
            _disposables = new();

            require.Ready
                .Subscribe(view.SetRight)
                .AddTo(_disposables);

            view.OnFixButtonClicked
                .Subscribe(_ => OnFixClicked(require))
                .AddTo(_disposables);
        }


        public void Dispose() => 
            _disposables.Dispose();

        protected virtual void OnFixClicked(T require) { }
    }
}
