using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Corsola : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Corsola")
            .SetStats(12, 1, 4)
            .SetSprites(
                Mod.GetSprite("Corsola"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(4)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(OnCardPlayedLoseHealth.Name),
                    Mod.SStack("On Turn Apply Shell To Self", 4),
                    Mod.SStack(StatusEffects.Cursola.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}