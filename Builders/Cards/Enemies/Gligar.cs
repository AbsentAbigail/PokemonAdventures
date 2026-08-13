using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Gligar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Gligar")
            .WithCardType("Enemy")
            .SetStats(4, 3, 3)
            .SetSprites(
                Mod.GetSprite("Gligar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(5)
            .EvolvesInto(Gliscor.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack("Shroom", 3),
                    Mod.SStack(ShroomImmune.Name),
                    Mod.SStack(WhileShroomedImmuneToDebuffs.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}