using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Companions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Onix : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Onix")
            .WithCardType("Enemy")
            .SetStats(4, 2, 3)
            .SetSprites(
                Mod.GetSprite("Onix"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(7)
            .EvolvesInto(Steelix.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(Constricted.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("Shell", 5),
                ];
                card.traits =
                [
                    Mod.TStack("Smackback"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}