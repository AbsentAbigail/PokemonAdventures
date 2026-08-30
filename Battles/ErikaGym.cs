using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;
using PokemonMod.Builders.Cards.FieldEffects;

namespace PokemonMod.Battles;

public static class ErikaGym
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "ErikaGym", 0)
            .SetSprite("FightErika.png")
            .SetNameRef("Celadon City Gym")
            .AddBattleToLoader().LoadBattle(2, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('E', ErikaAndVileplume.Name),
                ('X', Exeggcute.Name),
                ('K', Sunkern.Name),
                ('B', Bellsprout.Name),
                ('S', Shuckle.Name),
                ('O', Oddish.Name),
                ('W', HarshSunlight.Name)
            ).StartWavePoolData(0, "Wave 1: Erika")
            .ConstructWaves(3, 0, "EXKW")
            .StartWavePoolData(1, "Wave 2: Bellsprout")
            .ConstructWaves(3, 1, "BSK", "BSW")
            .StartWavePoolData(2, "Wave 3: Oddish")
            .ConstructWaves(3, 2, "OOX")
            .GiveMiniBossesCharms([ErikaAndVileplume.Name], "CardUpgradeHeart");
    }
}