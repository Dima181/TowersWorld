using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit
{
    [RequireComponent(typeof(Button))]
    public class MarkedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _markState;

        public IObservable<Unit> OnClick => _button.OnClickAsObservable();

        private void OnValidate()
        {
            if(_button == null)
                _button = GetComponent<Button>();
        }

        public void SetMark(bool visible)
            => _markState.SetActive(visible);
    }
}
