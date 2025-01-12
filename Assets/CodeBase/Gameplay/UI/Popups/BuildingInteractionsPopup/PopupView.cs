using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Popups.BuildingInteractionsPopup
{
    public class PopupView : UIScreenView, IPopupView, ISerializationCallbackReceiver
    {
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();

        public IObservable<Unit> GetButtonObservable(EButtonId buttonId) =>
            GetButtonDataById(buttonId).Button.OnClickAsObservable();

        public void ShowButton(EButtonId buttonId) =>
            ShowButtonIntarnal(GetButtonDataById(buttonId));

        public void HideButton(EButtonId buttonId) =>
            HideButtonInternal(GetButtonDataById(buttonId));

        public IReadOnlyList<IObservable<Unit>> AllButtonsClickObservables =>
            _buttonsData.Select(data => data.Button.OnClickAsObservable()).ToArray();

        public void HideAllButtons()
        {
            foreach (var buttounData in _buttonsData)
            {
                HideButtonInternal(buttounData);
            }
        }

        [Serializable]
        private sealed class ButtonData
        {
            public EButtonId Id;
            public Button Button;
            public TextMeshProUGUI Text;
        }

        [SerializeField] private Button _closeButton;

        [SerializeField] private ButtonData[] _buttonsData = Array.Empty<ButtonData>();

        private Dictionary<EButtonId, ButtonData> _buttonsDataById;

        private ButtonData GetButtonDataById(EButtonId buttonId)
        {
            if (!_buttonsDataById.TryGetValue(buttonId, out ButtonData buttonData))
            {
                throw new ArgumentException($"No {nameof(ButtonData)} for id {buttonId}");
            }

            return buttonData;
        }

        private void ShowButtonIntarnal(ButtonData buttonData)
        {
            var buttonGameObejct = buttonData.Button.gameObject;
            buttonGameObejct.SetActive(true);
        }

        private void HideButtonInternal(ButtonData buttonData)
        {
            var buttonGameObject = buttonData.Button.gameObject;
            buttonGameObject.SetActive(false);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _buttonsDataById ??= new Dictionary<EButtonId, ButtonData>();
            _buttonsDataById.Clear();

            foreach (var buttonData in _buttonsData)
            {
                _buttonsDataById[buttonData.Id] = buttonData;
            }
        }

    }
}
