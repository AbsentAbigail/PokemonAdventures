using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Rhyhorn : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Rhyhorn")
            .WithCardType("Enemy")
            .SetStats(5, 3, 6)
            .SetSprites(
                Mod.GetSprite("Rhyhorn"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(6)
            .EvolvesInto(Rhydon.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack("Shell"),
                    Mod.SStack("Bonus Damage Equal To Shell"),
                    Mod.SStack(ParalysisResist.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Barrage"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}