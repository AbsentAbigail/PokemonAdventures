using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class Battles
{
    public static void AddBattles()
    {
        // Fight 1
        YoungsterFight.AddBattle();
        BugCatcherFight.AddBattle();
        
        // Fight 2
        LassFight.AddBattle();
        CamperFight.AddBattle();
        
        // Fight 3
        MistyGym.AddBattle();
        ErikaGym.AddBattle();
        
        // Fight 4
        BikerFight.AddBattle();
        
        // Fight 5
        
        // Fight 6
        
        // Fight 7
        
        // Eye of the Storm
        
        // Heart of the Storm
    }

    public static CardData[] GoldGivers() => [
        Mod.GetCard(Meowth.Name),
        Mod.GetCard(Delibird.Name),
    ];
}