using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Noctowl : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Noctowl")
            .WithCardType("Enemy")
            .SetStats(10, 2, 4)
            .SetSprites(
                Mod.GetSprite("Noctowl"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(Insomnia.Name),
                    Mod.SStack(OnCardPlayedDrawForSleepingAlliesAndEnemies.Name),
                    Mod.SStack(WhenSleepEnemyTakesDamageGainEqualHeal.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}