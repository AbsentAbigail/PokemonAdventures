using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;

namespace PokemonMod.Battles;

public static class KantoYoungster
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "Kanto Youngster")
            .SetSprite("FightKantoYoungster.png")
            .SetNameRef("KantoYoungster")
            .AddBattleToLoader().LoadBattle(0, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('P', Pidgey.Name),
                ('S', Spearow.Name),
                ('R', Rattata.Name),
                ('M', Mankey.Name),
                ('O', Oddish.Name),
                ('K', Builders.Cards.Enemies.KantoYoungster.Name)
            ).StartWavePoolData(0, "Wave 1: Pidgey")
            .ConstructWaves(3, 0, "PSR", "PRR")
            .StartWavePoolData(1, "Wave 2: Support")
            .ConstructWaves(3, 1, "M", "S")
            .StartWavePoolData(2, "Wave 3: Boss")
            .ConstructWaves(3, 2, "MOK", "PSK", "ROK")
            .GiveMiniBossesCharms([Builders.Cards.Enemies.KantoYoungster.Name], "CardUpgradeHeart")
            .FreeModify<BattleData>(battle =>
            {
                battle.goldGiverPool = [
                    Mod.GetCard(Meowth.Name),
                ];
            });
    }
}