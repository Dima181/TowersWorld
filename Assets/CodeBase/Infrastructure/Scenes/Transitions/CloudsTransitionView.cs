using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Transitions
{
    public class CloudsTransitionView : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [Header("Animation Settings")]
        [SerializeField] private Vector2 _inPosition;
        [SerializeField] private Vector2 _outPosition;
        [SerializeField] private float _inDuration;
        [SerializeField] private float _outDuration;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;

        public void ResetPosition() =>
            _transform.anchoredPosition = _outPosition;

        public async UniTask SetActive()
        {
            gameObject.SetActive(true);

            // Используем AsyncWaitForCompletion для ожидания завершения анимации
            await _transform
                .DOAnchorPos(_inPosition, _inDuration)
                .SetEase(_inEase)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
        }

        public async UniTask SetInactive()
        {
            await _transform
                .DOAnchorPos(_outPosition, _outDuration)
                .SetEase(_outEase)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}