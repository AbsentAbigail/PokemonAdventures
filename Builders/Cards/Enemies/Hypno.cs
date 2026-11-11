using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Hypno : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Hypno")
            .WithCardType("Enemy")
            .SetStats(9, 4, 5)
            .SetSprites(
                Mod.GetSprite("Hypno"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(5)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(Sleep.Name, 2),
                    Mod.SStack(InstantDealDamageEqualToSleep.Name),
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