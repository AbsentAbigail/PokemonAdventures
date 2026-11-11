using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Rhydon : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Rhydon")
            .WithCardType("Enemy")
            .SetStats(7, 4, 6)
            .SetSprites(
                Mod.GetSprite("Rhydon"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(7)
            .EvolvesInto(Rhyperior.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack("Shell", 2),
                    Mod.SStack("Bonus Damage Equal To Shell"),
                    Mod.SStack(WhenHitByElectricGainShell.Name),
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