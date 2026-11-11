using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class BikerFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Biker Fight")
            .SetSprite("FightBiker.png")
            .SetNameRef("Biker")
            .AddBattleToLoader().LoadBattle(3, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('E', Ekans.Name),
                ('A', Ariados.Name),
                ('P', Phanpy.Name),
                ('Z', Zubat.Name),
                ('G', Gliscor.Name),
                ('B', TrainerBiker.Name)
            ).StartWavePoolData(0, "Wave 1: Ariados")
            .ConstructWaves(3, 0, "AAP", "AEP")
            .StartWavePoolData(1, "Wave 2: Zubat")
            .ConstructWaves(3, 1, "ZE", "ZZ")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "EGPB", "EAGB")
            .GiveMiniBossesCharms([TrainerBiker.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = Battles.GoldGivers();
            });
    }
}