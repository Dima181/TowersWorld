using CodeBase.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.UI
{
    public class UILevelHealthItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _healthText;

        [Space][SerializeField] private Image _backgroundImage;

        [Space][SerializeField] private GameObject _activeImage;

        public void Active(bool active) => 
            _activeImage?.SetActive(active);

        public void Construct(int level, int health)
        {
            _levelText.SetTextFormated(level);
            _healthText.SetTextFormated(health);

            _backgroundImage.color = _backgroundImage.color.WithAlpha(level % 2 == 0 ? 0.05f : 0);
        }
    }
}
