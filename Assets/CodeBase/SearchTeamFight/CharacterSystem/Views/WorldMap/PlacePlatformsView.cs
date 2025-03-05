using System.Collections.Generic;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class PlacePlatformsView : MonoBehaviour
    {
        public List<EnemyPlatform> EnemyPlatforms => _enemyPlatforms;
        public List<CharacterPlatform> PlayerPlatforms => _playerPlatforms;
        
        public FighterPlaceholderConfig DragSpritePrefab => _dragSpritePrefab;


        [SerializeField] private List<EnemyPlatform> _enemyPlatforms;
        [SerializeField] private List<CharacterPlatform> _playerPlatforms;

        [SerializeField] private FighterPlaceholderConfig _dragSpritePrefab;
    }
}
