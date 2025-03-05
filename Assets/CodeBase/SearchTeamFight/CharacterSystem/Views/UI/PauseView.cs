using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class PauseView : MonoBehaviour
    {
        public IObservable<Unit> OnContinueClicked => _continueButton.OnClickAsObservable();
        public IObservable<Unit> OnRestartClicked => _restartButton.OnClickAsObservable();
        public IObservable<Unit> OnBackClicked => _backButton.OnClickAsObservable();

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _backButton;

        private float _cachedTimeScale = 1.0f;

        public void Show()
        {
            gameObject.SetActive(true);
            _cachedTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void Hide()
        {
            Time.timeScale = _cachedTimeScale;
            gameObject.SetActive(false);
        }

        private void OnDestroy() => 
            Time.timeScale = 1;
    }
}
