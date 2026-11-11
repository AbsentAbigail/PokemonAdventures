using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Nidoking : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Nidoking")
            .WithCardType("Enemy")
            .SetStats(8, 3, 4)
            .SetSprites(
                Mod.GetSprite("Nidoking"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(12)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("When Hit Apply Shroom To Attacker", 5),
                    Mod.SStack(OnKillIncreaseEffects.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}