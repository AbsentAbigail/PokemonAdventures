using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Houndour : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Houndour")
            .WithCardType("Enemy")
            .SetStats(5, 3, 3)
            .SetSprites(
                Mod.GetSprite("Houndour"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(5)
            .EvolvesInto(Houndoom.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Dark.Name),
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(WhenCardDestroyedApplyBurnToRandomEnemyInRow.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}