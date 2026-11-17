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
public class Magcargo : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Magcargo")
            .SetStats(14, 0, 6)
            .SetSprites(
                Mod.GetSprite("Magcargo"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(6)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(WhenHitApplyBurnToAttacker.Name, 2),
                ];
                card.traits =
                [
                    Mod.TStack(Incinerate.Name, 4),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}