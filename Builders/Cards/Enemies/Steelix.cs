using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Steelix : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Steelix")
            .WithCardType("Enemy")
            .SetStats(5, 4, 3)
            .SetSprites(
                Mod.GetSprite("Steelix"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(9)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(Constricted.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Steel.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("Shell", 6),
                    Mod.SStack(WhenConstrictedEnemyIsKilledGainBlock.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Smackback"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}