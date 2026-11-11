using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class BugCatcherFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Bug Catcher Fight")
            .SetSprite("FightBugCatcher.png")
            .SetNameRef("BugCatcher")
            .AddBattleToLoader().LoadBattle(0, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('L', Ledyba.Name),
                ('S', Spinarak.Name),
                ('C', Caterpie.Name),
                ('N', Nidoran.Name),
                ('W', Weedle.Name),
                ('B', TrainerBugCatcher.Name)
            ).StartWavePoolData(0, "Wave 1: Ledyba")
            .ConstructWaves(3, 0, "LLS", "LCS")
            .StartWavePoolData(1, "Wave 2: Nidoran")
            .ConstructWaves(3, 1, "NN")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "WWCCLB", "WWCSB")
            .GiveMiniBossesCharms([TrainerYoungster.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = Battles.GoldGivers();
            });
    }
}