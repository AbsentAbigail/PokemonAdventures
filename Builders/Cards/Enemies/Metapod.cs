using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Metapod : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Metapod")
            .WithCardType("Enemy")
            .SetStats(4, 0, 4)
            .SetSprites(
                Mod.GetSprite("Metapod"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(3)
            .EvolvesInto(Butterfree.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(OnCardPlayedConsumeBerriesInHand.Name, 2),
                    Mod.SStack(WhenBerryConsumedGainShell.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}