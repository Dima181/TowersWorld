using UnityEngine;
using DG.Tweening;

namespace UI.UI_Animation_scripts
{
    public class UIPopupAnimation : MonoBehaviour
    { public float Duration => _duration;
        [Header("Panels")]
        [SerializeField] private RectTransform _popupPanelRectTransform;
        [SerializeField] private GameObject _closePanel;

        [Header("Animation settings")]
        [SerializeField] private Ease _easeType = Ease.InOutExpo;
        [Range(0, 1)]
        [SerializeField] private float _duration = .2f;

        private Vector2 _defaultAnchoredPosition;
        public bool IsOpening { get; private set; }
        public bool IsInitialized { get; private set; }

        private void Init()
        {
            if(IsInitialized)
                return;
            
            _defaultAnchoredPosition = _popupPanelRectTransform.anchoredPosition;
            _popupPanelRectTransform.anchoredPosition = new Vector2(_popupPanelRectTransform.anchoredPosition.x, -Screen.height);
            IsOpening = false;
            IsInitialized = true;
            
        }

        public void OpenPopup()
        {
            Init();
            
            if (!IsOpening)
            {
                var startAnchoredPosition = _popupPanelRectTransform.anchoredPosition;
                if (startAnchoredPosition == _defaultAnchoredPosition)
                {
                    startAnchoredPosition = new Vector2(_popupPanelRectTransform.anchoredPosition.x, -Screen.height);
                    _popupPanelRectTransform.anchoredPosition = startAnchoredPosition;
                }

                _popupPanelRectTransform.gameObject.SetActive(true);
                _closePanel.SetActive(true);

                _popupPanelRectTransform.DOAnchorPosY(_defaultAnchoredPosition.y, _duration).SetEase(_easeType);
                IsOpening = true;
            }
        }

        public void ClosePopup()
        {
            if (IsOpening)
            {
                var targetAnchoredPosition = new Vector2(_popupPanelRectTransform.anchoredPosition.x, -Screen.height);

                _popupPanelRectTransform.DOAnchorPosY(targetAnchoredPosition.y, _duration).SetEase(_easeType).OnComplete(() =>
                {
                    _popupPanelRectTransform.gameObject.SetActive(false);
                });
                IsOpening = false;

                _closePanel.SetActive(false);
            }
        }
    }
}
