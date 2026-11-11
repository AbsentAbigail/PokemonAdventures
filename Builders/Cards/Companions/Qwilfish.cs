using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Qwilfish : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Qwilfish")
            .SetStats(8, 0, 4)
            .SetSprites(
                Mod.GetSprite("Qwilfish"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(4)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Shroom", 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenEnemyEntersBattleApplyShroom.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}