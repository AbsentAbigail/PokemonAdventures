using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Delibird : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Delibird")
            .WithCardType("Enemy")
            .SetStats(5, 2, 3)
            .SetSprites(
                Mod.GetSprite("Delibird"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ice.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(OnCardPlayedAddPresentToHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}