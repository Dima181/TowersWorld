using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class FighterHealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void Setup(int maxValue)
        {
            _healthSlider.wholeNumbers = true;
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = maxValue;
            _healthSlider.value = maxValue;
        }

        public void Show() =>
            _healthSlider.gameObject.SetActive(true);

        public void Hide() =>
            _healthSlider.gameObject.SetActive(false);

        public void SetValue(int value) => 
            _healthSlider.value = value;
    }
}