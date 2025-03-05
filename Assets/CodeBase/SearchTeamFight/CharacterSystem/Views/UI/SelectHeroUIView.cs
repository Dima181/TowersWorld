using AYellowpaper.SerializedCollections;
using Heroes.Model;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class SelectHeroUIView : MonoBehaviour
    {
        public IObservable<Unit> OnSelectCLicked => _selectButton.OnClickAsObservable();

        [SerializeField] private Image _heroIcon;
        [SerializeField] private Image _heroClass;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _applyView;

        [SerializeField] private TextMeshProUGUI _levelText;

        [SerializeField] private string _levelFormat = "Lv.<b>{0}<b/>";

        [SerializedDictionary("Fighter Attack Type", "Range"), SerializeField]
        private SerializedDictionary<EHeroClass, Sprite> _classSprites = new();

        public void UpdateSelectView(bool isActive) =>
            _applyView.SetActive(isActive);

        public void SetHeroIcon(Sprite heroIcon) =>
            _heroIcon.sprite = heroIcon;

        public void SetSpriteHeroClass(EHeroClass heroClass)
        {
            _classSprites ??= new();

            if(_classSprites.TryGetValue(heroClass, out var sprite))
            {
                _heroClass.sprite = sprite;
            }
            else
            {
                Debug.LogError("Sprite for hero class " + heroClass + " not found.");
            }
        }

        public void SetLevel(int value) =>
            _levelText.text = string.Format(_levelFormat, value);
    }
}
