using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Crobat : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Crobat")
            .WithCardType("Enemy")
            .SetStats(7, 2, 3)
            .SetSprites(
                Mod.GetSprite("Crobat"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(7)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack("MultiHit"),
                    Mod.SStack(OnCardPlayedHealSelf.Name, 3),
                    Mod.SStack(OnHitDealDamageToShroomedTarget.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}