using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Rapidash : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Rapidash")
            .WithCardType("Enemy")
            .SetStats(8, 2, 2)
            .SetSprites(
                Mod.GetSprite("Rapidash"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack("When Hit Apply Spice To Self", 5),
                    Mod.SStack(WhenDestroyedApplyBurnToEnemies.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}