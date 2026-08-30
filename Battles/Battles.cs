namespace PokemonMod.Battles;

public static class Battles
{
    public static void AddBattles()
    {
        // Fight 1
        YoungsterFight.AddBattle();
        
        // Fight 2
        LassFight.AddBattle();
        
        // Fight 3
        MistyGym.AddBattle();
        ErikaGym.AddBattle();
    }
}