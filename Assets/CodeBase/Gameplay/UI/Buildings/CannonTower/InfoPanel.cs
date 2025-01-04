using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Buildings.CannonTower
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Start() => 
            _closeButton.onClick.AddListener(() => ShowHide(false));

        public void ShowHide(bool isActive) => 
            gameObject.SetActive(isActive);
    }
}
