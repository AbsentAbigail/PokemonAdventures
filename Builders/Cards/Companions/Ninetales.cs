using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Ninetales : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ninetales")
            .SetStats(5, 1, 3)
            .SetSprites(
                Mod.GetSprite("Ninetales"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack("MultiHit", 2),
                    Mod.SStack(WhenAllyHitGainEqualSpice.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}