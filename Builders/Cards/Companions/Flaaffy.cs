using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Flaaffy : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Flaaffy")
            .SetStats(5, 0, 6)
            .SetSprites(
                Mod.GetSprite("Flaaffy"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Ampharos.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Weakness", 4),
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