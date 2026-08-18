using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class ErikaAndVileplume : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Erika and Vileplume")
            .WithCardType("Boss")
            .SetStats(15, 4, 4)
            .SetSprites(
                Mod.GetSprite("ErikaAndVileplume"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(OnCardPlayedAddLumBerryToHand.Name),
                    Mod.SStack(WhenBerryPlayedRestoreOwnHealth.Name, 2),
                    Mod.SStack(MistyAndStarmiePhase2.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Heartburn"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}