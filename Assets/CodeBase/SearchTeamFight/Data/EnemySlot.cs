using Heroes.Model;
using UnityEngine.Serialization;
using UnityEngine;
using UniRx;

namespace SearchTeamFight.Data
{
    public class EnemySlot
    {
        [SerializeField]
        [FormerlySerializedAs("Hero")]
        private Hero _hero;

        public ReactiveProperty<HeroConfig> HeroConfig = new ReactiveProperty<HeroConfig>();

        public Hero Hero
        {
            get
            {
                if(_hero.ID == EHero.None)
                {
                    _hero.ChangeId(HeroConfig.Value.ID);
                }

                return _hero;
            }
        }
        
        public EnemySlot(Hero hero)
        {
            _hero = hero;
        }
    }
}
