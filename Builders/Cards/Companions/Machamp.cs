using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Machamp : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Machamp")
            .SetStats(8, 2, 5)
            .SetSprites(
                Mod.GetSprite("Machamp"),
                Mod.GetBackgroundSprite(BackgroundSprites.Gym))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fighting.Name),
                    Mod.SStack(OnCardPlayedAddBulletPunchToHand.Name, 2),
                    Mod.SStack(WhenBulletPunchPlayedGainAttack.Name, 2),
                    Mod.SStack(WhileActiveBulletPunchHaveEqualDamage.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}