using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Mareep : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Mareep")
            .SetStats(3, 0, 6)
            .SetSprites(
                Mod.GetSprite("Mareep"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Flaaffy.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Weakness", 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Electric.Name),
                    Mod.SStack("When Hit Apply Weakness To Attacker"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}