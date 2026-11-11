using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Nidoqueen : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Nidoqueen")
            .WithCardType("Enemy")
            .SetStats(8, 3, 4)
            .SetSprites(
                Mod.GetSprite("Nidoqueen"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(12)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(WhenHitApplyShroomToEnemies.Name, 2),
                    Mod.SStack(OnKillIncreaseEffects.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}