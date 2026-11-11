using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Zubat : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Zubat")
            .WithCardType("Enemy")
            .SetStats(4, 2, 3)
            .SetSprites(
                Mod.GetSprite("Zubat"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(3)
            .EvolvesInto(Golbat.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(OnCardPlayedHealSelf.Name),
                    Mod.SStack(OnHitDealDamageToShroomedTarget.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}