using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class PunkCoupleFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Punk Couple Fight")
            .SetSprite("FightPunkCouple.png")
            .SetNameRef("Punk Couple")
            .AddBattleToLoader().LoadBattle(3, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('A', Arbok.Name),
                ('V', Misdreavus.Name),
                ('H', Houndour.Name),
                ('O', Nidorino.Name),
                ('N', Nidorina.Name),
                ('P', Ponyta.Name),
                ('M', Murkrow.Name),
                ('C', TrainerPunkCouple.Name)
            ).StartWavePoolData(0, "Wave 1: Misdreavous and Houndour")
            .ConstructWaves(3, 0, "AVH", "OHV")
            .StartWavePoolData(1, "Wave 2: Nido")
            .ConstructWaves(3, 1, "NV", "OP")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "HVMMC", "AVVMC", "HVVMC")
            .GiveMiniBossesCharms([TrainerPunkCouple.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = Battles.GoldGivers();
            });
    }
}