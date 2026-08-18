using BattleEditor;
using PokemonMod.Builders.Cards.Enemies;
using PokemonMod.Builders.Cards.FieldEffects;

namespace PokemonMod.Battles;

public static class MistyGym
{
    public static void AddBattle()
    {
        new BattleDataEditor(Mod.Instance, "MistyGym", 0)
            .SetSprite("FightMisty.png")
            .SetNameRef("Cerulean City Gym")
            .AddBattleToLoader().LoadBattle(2, exclusivity: BattleStack.Exclusivity.removeAll)
            .EnemyDictionary(
                ('B', MistyAndStarmie.Name),
                ('S', Shellder.Name),
                ('M', Mantine.Name),
                ('H', Horsea.Name),
                ('R', Remoraid.Name),
                ('Y', Staryu.Name),
                ('W', Rain.Name)
            ).StartWavePoolData(0, "Wave 1: Misty")
            .ConstructWaves(3, 0, "SMBW")
            .StartWavePoolData(1, "Wave 2: Horsea")
            .ConstructWaves(3, 1, "HRR", "HRW")
            .StartWavePoolData(2, "Wave 3: Staryu")
            .ConstructWaves(3, 2, "YYH")
            .GiveMiniBossesCharms([TrainerYoungster.Name], "CardUpgradeHeart");
    }
}