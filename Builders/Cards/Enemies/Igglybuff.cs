using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Igglybuff : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Igglybuff")
            .SetStats(5, 1, 6)
            .SetSprites(
                Mod.GetSprite("Igglybuff"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(4)
            .EvolvesInto(Jigglypuff.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Types.Fairy.Name),
                    Mod.SStack(WhenHitApplySleepToAttacker.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}