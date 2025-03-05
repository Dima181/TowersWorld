using Heroes.Model;
using System.Collections.Generic;

namespace REST.Heroes.Models
{
    public class HeroesModel
    {
        public Dictionary<EHero, Hero> Heroes { get; set; }
        public Dictionary<EHero, Hero> HeroesBackup { get; set; }

        public HeroesModel(Dictionary<EHero, Hero> heroes)
        {
            Heroes = heroes;
        }
    }
}
