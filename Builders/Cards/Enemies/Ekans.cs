using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Ekans : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ekans")
            .WithCardType("Enemy")
            .SetStats(5, 1, 4)
            .SetSprites(
                Mod.GetSprite("Ekans"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pollution))
            .DropsBling(6)
            .EvolvesInto(Arbok.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(InstantApplyShroomEqualToDebuffs.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenHitApplyFrostToAttacker.Name),
                    Mod.SStack(ShroomResist.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}