using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class FighterPlaceholderConfig : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _correctDragColor;
        [SerializeField] private Color _uncorrectDragColor;
        public Vector3 PlaceHolderOffset;

        public Color CorrectDragColor => _correctDragColor;
        public Color UncorrectDragColor => _uncorrectDragColor;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

    }
}
