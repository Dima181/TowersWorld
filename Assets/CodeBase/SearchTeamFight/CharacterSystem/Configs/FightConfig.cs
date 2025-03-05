using Heroes.Model;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Configs
{
    [CreateAssetMenu(menuName = "Configs/Fight/" + nameof(FightConfig), fileName = nameof(FightConfig))]
    public class FightConfig : ScriptableObject
    {


        public SerializedDictionary<EHero, GameObject> HeroPrefabsById => _heroPrefabsById;
        /*public SerializedDictionary<TypesTeam, GameObject> TeamCircleById => _teamCircleById;*/

        [SerializedDictionary("Enum Hero", "Figher Prefab"), SerializeField]
        private SerializedDictionary<EHero, GameObject> _heroPrefabsById = new SerializedDictionary<EHero, GameObject>();

        private void Awake()
        {
            if (_heroPrefabsById == null)
                _heroPrefabsById = new SerializedDictionary<EHero, GameObject>();

        }

        /*[SerializedDictionary("HeroType", "Circle Prefab"), SerializeField]
        private SerializedDictionary<TypesTeam, GameObject> _teamCircleById = default;*/

        /*private HeroConfig _playerHeroesConfig;
        private HeroConfig _enemyHeroesConfig;
        private HeroConfig _creepsHeroesConfig;

        public HeroesConfig PlayerHeroesConfig;
        public HeroesConfig EnemyHeroesConfig;
        public HeroesConfig CreepsHeroesConfig;*/
    }
}
