using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Drowzee : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Drowzee")
            .WithCardType("Enemy")
            .SetStats(7, 2, 5)
            .SetSprites(
                Mod.GetSprite("Drowzee"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(5)
            .EvolvesInto(Hypno.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(Sleep.Name, 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(Insomnia.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}