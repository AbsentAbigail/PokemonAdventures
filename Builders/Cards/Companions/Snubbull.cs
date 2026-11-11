using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Snubbull : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Snubbull")
            .SetStats(6, 4, 5)
            .SetSprites(
                Mod.GetSprite("Snubbull"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(7)
            .EvolvesInto(Granbull.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fairy.Name),
                    Mod.SStack(WhenDeployedApplyFrostToEnemies.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}