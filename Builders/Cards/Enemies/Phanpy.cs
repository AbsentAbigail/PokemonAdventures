using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Phanpy : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Phanpy")
            .WithCardType("Enemy")
            .SetStats(6, 2, 3)
            .SetSprites(
                Mod.GetSprite("Phanpy"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(5)
            .EvolvesInto(Donphan.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(OnCardPlayedKillClunkers.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}