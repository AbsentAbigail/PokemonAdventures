using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Shuckle : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Shuckle")
            .WithCardType("Enemy")
            .SetStats(1, 0, 6)
            .SetSprites(
                Mod.GetSprite("Shuckle"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(3)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack("Shell", 10),
                    Mod.SStack(WhenBerryPlayedRestoreHealthOfAllies.Name),
                    Mod.SStack(OnCardPlayedSunnyAddGanlonBerryToHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}