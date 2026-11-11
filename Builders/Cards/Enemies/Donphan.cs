using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Donphan : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Donphan")
            .WithCardType("Enemy")
            .SetStats(6, 4, 3)
            .SetSprites(
                Mod.GetSprite("Donphan"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(7)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(OnCardPlayedTransferScrapFromAlliedAndEnemyClunkersToSelf.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}