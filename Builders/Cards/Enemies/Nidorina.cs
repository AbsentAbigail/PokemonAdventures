using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Nidorina : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Nidorina")
            .WithCardType("Enemy")
            .SetStats(7, 1, 4)
            .SetSprites(
                Mod.GetSprite("Nidorina"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(8)
            .EvolvesInto(Nidoqueen.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenHitApplyShroomToEnemiesInRow.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}