using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Nidorino : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Nidorino")
            .WithCardType("Enemy")
            .SetStats(7, 1, 4)
            .SetSprites(
                Mod.GetSprite("Nidorino"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(8)
            .EvolvesInto(Nidoking.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack("When Hit Apply Shroom To Attacker", 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}