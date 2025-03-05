using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class AbilityPopupView : MonoBehaviour
    {
        [SerializeField] private Image _abilityPopup;
        [SerializeField] private Vector2 _startAnchorPosotion;
        [SerializeField] private Vector2 _endAnchorPosotion;
        [SerializeField] private float _popupShowDuration;
        [SerializeField] private float _popupStayDuration;
        [SerializeField] private float _popupHideDuration;

        public async UniTask ShowAbilityPopup(Sprite sprite)
        {
            if (_abilityPopup == null)
                return;

            _abilityPopup.sprite = sprite;
            _abilityPopup.gameObject.SetActive(true);
            _abilityPopup.rectTransform.anchoredPosition = _startAnchorPosotion;

            var color = _abilityPopup.color;
            color.a = 0;
            _abilityPopup.color = color;
            
            var sequence = DOTween.Sequence();
            sequence.Append(_abilityPopup.rectTransform.DOAnchorPos(_endAnchorPosotion, _popupShowDuration));
            sequence.Join(_abilityPopup.DOFade(1, _popupShowDuration));

            sequence.AppendInterval(_popupStayDuration);

            sequence.Append(_abilityPopup.DOFade(0, _popupHideDuration));

            await sequence.AwaitForComplete();

            _abilityPopup.gameObject.SetActive(false);
        }
    }
}
