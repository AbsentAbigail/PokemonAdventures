using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class TrainerCamper : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Camper")
            .WithCardType("Miniboss")
            .SetStats(14, null, 6)
            .DropsBling(14)
            .SetSprites(
                Mod.GetSprite("Camper"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedApplyShellToRockAllies.Name, 3),
                    Mod.SStack(WhenSandstormPlayedCountDownAlliedPokemon.Name),
                    Mod.SStack(BossRewardCamper.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}