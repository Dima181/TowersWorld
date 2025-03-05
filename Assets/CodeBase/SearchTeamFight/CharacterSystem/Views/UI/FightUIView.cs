using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class FightUIView : MonoBehaviour
    {
        public IObservable<Unit> OnStartFightClicked => _startFightButton.OnClickAsObservable();
        public IObservable<Unit> OnBackClicked => _backButton.OnClickAsObservable();
        public IObservable<Unit> OnAutoClicked => _autoButton.OnClickAsObservable();
        public IObservable<Unit> DoubleTimeClicked => _doubleTimeButton.OnClickAsObservable();
        public IObservable<Unit> OnPauseClicked => _pauseButton.OnClickAsObservable();

        [SerializeField] private Button _startFightButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _levelText;

        [SerializeField] private Button _autoButton;
        [SerializeField] private Button _doubleTimeButton;
        [SerializeField] private Image _autoButtonImage;
        [SerializeField] private Image _doubleTimeButtonImage;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        public void SetAutoButtonActivated(bool isActivated)
        {
            var sprite = isActivated ? _activeSprite : _inactiveSprite;
            _autoButtonImage.sprite = sprite;
        }

        public void SetDoubleTimeButtonActivated(bool isActivated)
        {
            var sprite = isActivated ? _activeSprite : _inactiveSprite;
            _doubleTimeButtonImage.sprite = sprite;
        }

        public void ShowPauseButton(bool isShow) =>
            _pauseButton.gameObject.SetActive(isShow);
    }
}
