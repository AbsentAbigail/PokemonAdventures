using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Haunter : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Haunter",
                idleAnim: "FloatAnimationProfile")
            .SetStats(4, 2, 4)
            .SetSprites(
                Mod.GetSprite("Haunter"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(4)
            .EvolvesInto(Gengar.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenAnythingSleepdDealDamageEqualToAttack.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}