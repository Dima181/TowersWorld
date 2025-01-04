using Zenject;

namespace UI.Core
{
    public static class UIBindingExtensions
    {
        public static PresenterBinderGeneric<TPresenter> BindPresenter<TPresenter>(this DiContainer di)
            where TPresenter : IUIScreenPresenter
        {
            return new PresenterBinderGeneric<TPresenter>(di.StartBinding());
        }

        public static SubPresenterBinderGeneric<TSubPresenter> BindSubPresenter<TSubPresenter>(this DiContainer di)
            where TSubPresenter : IUIScreenSubPresenter
        {
            return new SubPresenterBinderGeneric<TSubPresenter>(di.StartBinding());
        }
    }
}
