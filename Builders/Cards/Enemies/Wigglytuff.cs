using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Wigglytuff : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Wigglytuff")
            .WithCardType("Enemy")
            .SetStats(15, 2, 6)
            .SetSprites(
                Mod.GetSprite("Wigglytuff"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Types.Fairy.Name),
                    Mod.SStack(WhenHitApplySleepToAttacker.Name, 5),
                    Mod.SStack(WhenEnemyIsSleepedRestoreHealth.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}