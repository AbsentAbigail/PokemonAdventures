using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Kakuna : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Kakuna")
            .WithCardType("Enemy")
            .SetStats(4, 0, 3)
            .SetSprites(
                Mod.GetSprite("Kakuna"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Beedrill.Name)
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
                    Mod.SStack("Shell"),
                    Mod.SStack(WhenBerryPlayedGainShell.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}