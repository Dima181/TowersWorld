using System;
using UnityEngine;

namespace UI.GameScreens.ScreenHud
{
    public enum EHudButton
    {
        None,

        Exploration,
    }

    /*public interface IUIHudScreenView
    {
        IObservable<EHudButton> OnHudButtonClick { get; }

        void ShouHide(bool isShowing);
        public RectTransform GetButtonTransform(EHudButton buttonType);
    }*/

    /*public class UIHudScreen : MonoBehaviour, IUIHudScreenView
    {
        public IObservable<EHudButton> OnHudButtonClick => throw new NotImplementedException();

        public RectTransform GetButtonTransform(EHudButton buttonType)
        {
            throw new NotImplementedException();
        }

        public void ShouHide(bool isShowing)
        {
            throw new NotImplementedException();
        }
    }*/
}
