using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.CannonTower.Views.DetailedScreen
{
    public class UICannonTowerDetailed : MonoBehaviour
    {
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();

        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private GameObject _item1;
        [SerializeField] private Sprite _sprite1;
        [SerializeField] private Sprite _sprite2;
        [SerializeField] private Sprite _sprite3;
        [SerializeField] private Transform _parrent;
    }
}
