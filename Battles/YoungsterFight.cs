using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class YoungsterFight
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Youngster Fight")
            .SetSprite("FightYoungster.png")
            .SetNameRef("Youngster")
            .AddBattleToLoader().LoadBattle(0, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('P', Pidgey.Name),
                ('S', Spearow.Name),
                ('R', Rattata.Name),
                ('G', Gligar.Name),
                ('W', Wooper.Name),
                ('Y', TrainerYoungster.Name)
            ).StartWavePoolData(0, "Wave 1: Pidgey")
            .ConstructWaves(3, 0, "PSR", "PPS")
            .StartWavePoolData(1, "Wave 2: Support")
            .ConstructWaves(3, 1, "GR", "WW")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "SSPY", "GRRY", "WSPY")
            .GiveMiniBossesCharms([TrainerYoungster.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = [
                    Mod.GetCard(Meowth.Name),
                ];
            });
    }
}