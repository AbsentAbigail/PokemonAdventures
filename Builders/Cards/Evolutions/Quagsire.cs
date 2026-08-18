using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Quagsire : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Quagsire")
            .WithCardType("Enemy")
            .SetStats(15, 2, 5)
            .SetSprites(
                Mod.GetSprite("Quagsire"),
                Mod.GetBackgroundSprite(BackgroundSprites.Bog))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(ImmuneToBomDemonizeSpiceFrost.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}