using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Scizor : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Scizor")
            .SetStats(10, 2)
            .SetSprites(
                Mod.GetSprite("Scizor"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(Types.Steel.Name),
                    Mod.SStack("MultiHit"),
                    Mod.SStack(WhenRedrawBellHitAddBulletPunchToHand.Name),
                    Mod.SStack(WhenBulletPunchPlayedTriggerAgainstTarget.Name),
                ];
                card.traits =
                [
                    // Mod.TStack(LeafGuard.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}