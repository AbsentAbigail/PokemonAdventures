using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Cloyster : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Cloyster")
            .WithCardType("Enemy")
            .SetStats(3, 2, 5)
            .SetSprites(
                Mod.GetSprite("Cloyster"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(6)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Ice.Name),
                    Mod.SStack("Shell", 9),
                    Mod.SStack(OnCardPlayedApplySnowToConstrictedEnemies.Name),
                    Mod.SStack(WhenHitApplyConstrictToAttacker.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}