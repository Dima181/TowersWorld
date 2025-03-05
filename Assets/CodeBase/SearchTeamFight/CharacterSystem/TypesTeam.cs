namespace SearchTeamFight.CharacterSystem
{
    public enum TypesTeam
    {
        Player = 0,
        Enemy = 1
    }

    public static class TypesTeamExtensions
    {
        public static TypesTeam GetOppositeTeam(this TypesTeam team)
        {
            if(team == TypesTeam.Player)
                return TypesTeam.Enemy;
            else
                return TypesTeam.Player;
        }
    }
}
