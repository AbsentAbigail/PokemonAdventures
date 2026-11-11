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
        
        // Fight 3
        MistyGym.AddBattle();
        ErikaGym.AddBattle();
    }

    public static CardData[] GoldGivers() => [
        Mod.GetCard(Meowth.Name),
        Mod.GetCard(Delibird.Name),
    ];
}