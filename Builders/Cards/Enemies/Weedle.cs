using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Weedle : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Weedle")
            .WithCardType("Enemy")
            .SetStats(3, 1, 3)
            .SetSprites(
                Mod.GetSprite("Weedle"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(3)
            .EvolvesInto(Kakuna.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(InstantApplyShroomEqualToBerries.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(Types.Poison.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}