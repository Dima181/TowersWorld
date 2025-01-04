using System;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UI.GameScreens.ScreenHud;
using TMPro;

namespace Gameplay.UI.HUD
{
    public class UIGameplayHUDView : UIScreenView
    {
        [Header("Botton panel")]
        [SerializeField] private Button _expeditionButton;

        [Header("Text resources")]
        [SerializeField] private TMP_Text _donateText;
        [SerializeField] private TMP_Text _meatText;
        [SerializeField] private TMP_Text _ironText;

        public void SetValueResourcesText(int donateValue, int meatValue, int ironValue)
        {
            _donateText.text = donateValue.ToString();
            _meatText.text = meatValue.ToString();
            _ironText.text = ironValue.ToString();
        }

        public IObservable<EHudButton> OnHudButtonClick
            =>
                Observable.Merge
                (
                    GetObservable(_expeditionButton, EHudButton.Exploration)
                );

        public Button GetButtonTransform(EHudButton buttonId) =>
            buttonId switch
            {
                EHudButton.Exploration => _expeditionButton
            };

        private static IObservable<EHudButton> GetObservable(Button hudButton, EHudButton hudButtonType) =>
            hudButton.OnClickAsObservable().Select(_ => hudButtonType);
    }
}
