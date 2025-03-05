using SearchTeamFight.CharacterSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class FighterInfoBarView : MonoBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Sprite[] _icons;
        [SerializeField] private Image _iconImage;

        public void Setup(FighterModel fighterModel)
        {
            int index = (int)fighterModel.Data.Class;
            _iconImage.sprite = _icons[index];
            _levelText.text = fighterModel.Data.Level.ToString();
        }

        public void SetVisible(bool value = true) => 
            _ui.gameObject.SetActive(value);
    }
}