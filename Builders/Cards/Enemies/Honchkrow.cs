using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Honchkrow : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Honchkrow")
            .WithCardType("Enemy")
            .SetStats(6, 4, 4)
            .SetSprites(
                Mod.GetSprite("Honchkrow"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Dark.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(OnKillIncreaseAttack.Name),
                    Mod.SStack(TriggerAgainstWhenDemonizeApplied.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}