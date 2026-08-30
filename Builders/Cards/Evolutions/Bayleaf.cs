using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;
using LeafGuard = PokemonMod.Builders.Traits.LeafGuard;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Bayleef : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Bayleef")
            .SetStats(6, 2, 3)
            .SetSprites(
                Mod.GetSprite("Bayleef"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Meganium.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(OnCardPlayedSunnyDoubleOwnAttack.Name),
                ];
                card.traits =
                [
                    Mod.TStack(LeafGuard.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}