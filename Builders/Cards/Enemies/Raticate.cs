using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Raticate : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Raticate")
            .WithCardType("Enemy")
            .SetStats(8, 3, 3)
            .SetSprites(
                Mod.GetSprite("Raticate"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                ];
                card.traits =
                [
                    Mod.TStack(Guts.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}