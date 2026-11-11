using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Nidoran : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Nidoran")
            .WithCardType("Enemy")
            .SetStats(6, 1, 4)
            .SetSprites(
                Mod.GetSprite("Nidoran"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(6)
            .EvolvesInto(Nidorina.Name)
            .EvolvesInto(Nidorino.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack("When Hit Apply Shroom To Attacker"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}