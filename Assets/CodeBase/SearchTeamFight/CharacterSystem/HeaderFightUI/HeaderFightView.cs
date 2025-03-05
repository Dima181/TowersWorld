using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.HeaderFightUI
{
    public class HeaderFightView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _strenghtEnemyText;
        [SerializeField] private TextMeshProUGUI _strenghtAllyText;
        [SerializeField] private TextMeshProUGUI _currentLevel;

        [SerializeField] private Image _enemyImage;
        [SerializeField] private Image _allyImage;

        /// <summary>
        /// This is the default sprite, in the future there will be logic for installing sprites for Ally.
        /// </summary>
        public Sprite DefaultAllySprite;
        public Sprite DefaultEnemySprite;

        public Sprite EnemyIcon
        {
            get => _enemyImage.sprite;
            set => _enemyImage.sprite = value;
        }

        public Sprite AllyIcon
        {
            get => _allyImage.sprite;
            set => _allyImage.sprite = value;
        }

        public void SetStrenghtEnemy(int strenght) =>
            _strenghtEnemyText.text = strenght.ToDisplayedString();

        public void SetStrenghtAlly(int strenght) =>
            _strenghtAllyText.text = strenght.ToDisplayedString();

        public void SetCurrentLevel(int level) => 
            _currentLevel.text = level.ToDisplayedString();
    }
}
