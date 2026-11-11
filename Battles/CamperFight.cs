using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class CamperFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Camper Fight")
            .SetSprite("FightCamper.png")
            .SetNameRef("Camper")
            .AddBattleToLoader()
            .LoadBattle(1, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('G', Geodude.Name),
                ('L', Larvitar.Name),
                ('B', Ledyba.Name),
                ('R', Rhyhorn.Name),
                ('O', Onix.Name),
                ('C', TrainerCamper.Name)
            ).StartWavePoolData(0, "Wave 1: Geodude and Larvitar")
            .ConstructWaves(3, 0, "GGL", "BGL")
            .StartWavePoolData(1, "Wave 2: Rhyhorn")
            .ConstructWaves(3, 1, "RR", "BR")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "OGRLC", "OGLC")
            .GiveMiniBossesCharms([TrainerCamper.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = Battles.GoldGivers();
            });
    }
}