using SearchTeamFight.CharacterSystem.Models;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class HeroUIView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Image _heroIcon;

        [SerializeField] private GameObject _deadGO;

        private FighterModel _fighterModel;

        public Image HeroIcon => _heroIcon;
        public FighterModel FighterModel => _fighterModel;

        public void SetFighterModel(FighterModel fighterModel) =>
            _fighterModel = fighterModel;

        public void SetHeroIcon(Sprite heroIcon) =>
            _heroIcon.sprite = heroIcon;

        public void SetupHealthSlider(int maxHealth)
        {
            _healthSlider.wholeNumbers = true;
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;
        }

        public void SetHealthSliderValue(int value) =>
            _healthSlider.value = value;

        public void SetDead() => 
            _deadGO.SetActive(true);
    }
}
