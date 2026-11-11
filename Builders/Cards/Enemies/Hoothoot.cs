using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Hoothoot : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Hoothoot")
            .WithCardType("Enemy")
            .SetStats(6, 1, 4)
            .SetSprites(
                Mod.GetSprite("Hoothoot"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(4)
            .EvolvesInto(Noctowl.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(Insomnia.Name),
                    Mod.SStack(WhenSleepEnemyTakesDamageGainEqualHeal.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}