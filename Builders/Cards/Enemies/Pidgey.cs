using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Pidgey : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Pidgey")
            .WithCardType("Enemy")
            .SetStats(3, 1, 2)
            .SetSprites(
                Mod.GetSprite("Pidgey"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(3)
            .EvolvesInto(Pidgeotto.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Types.Flying.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Knockback"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}