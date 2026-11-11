using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class LassFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Lass Fight")
            .SetSprite("FightLass.png")
            .SetNameRef("Lass")
            .AddBattleToLoader().LoadBattle(1, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('J', Jigglypuff.Name),
                ('N', Natu.Name),
                ('H', Hoothoot.Name),
                ('R', Nidoran.Name),
                ('D', Drowzee.Name),
                ('L', TrainerLass.Name)
            ).StartWavePoolData(0, "Wave 1: Jigglypuff and Natu")
            .ConstructWaves(3, 0, "JJN", "JNH")
            .StartWavePoolData(1, "Wave 2: Nidoran")
            .ConstructWaves(3, 1, "RR")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "HHDNL", "JHDNL", "HDDNL", "RHDNL")
            .GiveMiniBossesCharms([TrainerLass.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = Battles.GoldGivers();
            });
    }
}