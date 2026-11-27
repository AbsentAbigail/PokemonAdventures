using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Murkrow : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Murkrow")
            .WithCardType("Enemy")
            .SetStats(4, 4, 4)
            .SetSprites(
                Mod.GetSprite("Murkrow"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(5)
            .EvolvesInto(Honchkrow.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Dark.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(TriggerAgainstWhenDemonizeApplied.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}