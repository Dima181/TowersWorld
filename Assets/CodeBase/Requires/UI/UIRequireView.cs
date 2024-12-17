using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Requires.UI
{
    public abstract class UIRequireView : MonoBehaviour
    {
        public IObservable<Unit> OnFixButtonClicked => _fixButton.OnClickAsObservable();
        public virtual bool CanFix { get => true; }


        [SerializeField] private GameObject _rightState;
        [SerializeField] private GameObject _wrongState;
        [SerializeField] private Button _fixButton;
        [Space]
        [SerializeField] private Color _defaultRequireTextColor = Color.white;
        [SerializeField] private Color _wrongRequireTextColor = Color.red;


        public void SetRight(bool right)
        {
            _rightState.SetActive(right);
            _wrongState.SetActive(!right);

            _fixButton.gameObject.SetActive(!right && CanFix);
            SetRequireTextColor(right ? _defaultRequireTextColor : _wrongRequireTextColor);
        }

        protected abstract void SetRequireTextColor(Color color);
    }
}
