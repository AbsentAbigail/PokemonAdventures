using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Tentacool : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Tentacool")
            .SetStats(6, 1, 3)
            .SetSprites(
                Mod.GetSprite("Tentacool"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(4)
            .EvolvesInto(Tentacruel.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(OnCardPlayedApplyInkToConstrictedEnemies.Name, 2),
                    Mod.SStack(WhenDebuffedConstrictApplierAndCleanseSelf.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}