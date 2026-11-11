using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Vulpix : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Vulpix")
            .SetStats(4, 1, 3)
            .SetSprites(
                Mod.GetSprite("Vulpix"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(4)
            .InUnitPool()
            .EvolvesInto(Ninetales.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack("MultiHit"),
                    Mod.SStack(WhenAllyHitGainEqualSpice.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}