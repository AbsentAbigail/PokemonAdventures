using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Slugma : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Slugma")
            .SetStats(8, 0, 6)
            .SetSprites(
                Mod.GetSprite("Slugma"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(4)
            .InUnitPool()
            .EvolvesInto(Magcargo.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(WhenHitApplyBurnToAttacker.Name),
                ];
                card.traits =
                [
                    Mod.TStack(Incinerate.Name, 2),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}