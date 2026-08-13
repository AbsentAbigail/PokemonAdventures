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
public class Meganium : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Meganium")
            .SetStats(8, 2, 3)
            .SetSprites(
                Mod.GetSprite("Meganium"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(OnCardPlayedSunnyTripleOwnAttack.Name),
                ];
                card.traits =
                [
                    Mod.TStack(LeafGuard.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}