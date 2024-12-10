using Gameplay.Model;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Buildings.CannonTower.Views.Description
{
    public class UICannonTowerDescription : MonoBehaviour
    {
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();
        public IObservable<Unit> OnDetailClicked => _detailedButton.OnClickAsObservable();

        [SerializeField] private TextMeshProUGUI _logoText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _iconBuild;
        [SerializeField] private TextMeshProUGUI _levelBuild;
        [SerializeField] private Button _detailedButton;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [Space]
        [SerializeField] private Transform _parrent;
        [SerializeField] private GameObject _item;
        [SerializeField] private Sprite _sprite1;
        [SerializeField] private Sprite _sprite2;

        [Inject] private readonly BuildingModel _model;

        public void SetInformation(string logoText, string levelText, string description)
        {
            _logoText.text = logoText;
            _levelBuild.text = $"Lvl. {levelText}";
            _descriptionText.text = description;
        }

        public void GenerateItemInfo(int currentLevel)
        {
            if (_parrent.transform.childCount < 1)
            {
                GameObject item = Instantiate(_item, _parrent);

                item.transform.GetChild(0).GetComponent<TMP_Text>().text = "Здоровье";
                item.transform.GetChild(1).GetComponent<TMP_Text>().text = _model.HealthPerLevel[currentLevel].ToString();
                item.GetComponent<Image>().sprite = _sprite1;

                /*GameObject item2 = Instantiate(_item, _parrent);

                item2.transform.GetChild(0).GetComponent<TMP_Text>().text = "Сила";
                item2.transform.GetChild(1).GetComponent<TMP_Text>().text = _model.StorageCenterStats.Power.ValueAtLevel[currentLevel].Value.ToString();
                item2.GetComponent<Image>().sprite = _sprite2;*/
            }
        }


    }
}
