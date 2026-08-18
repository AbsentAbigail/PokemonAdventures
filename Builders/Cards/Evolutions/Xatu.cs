using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;
using BombardSingleTarget = PokemonMod.Builders.Traits.BombardSingleTarget;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Xatu : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Xatu")
            .WithCardType("Enemy")
            .SetStats(8, 4, 3)
            .SetSprites(
                Mod.GetSprite("Xatu"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(5)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(DealAdditionalDamage.Name, 6),
                    Mod.SStack(SleepResist.Name),
                ];
                card.traits =
                [
                    Mod.TStack(BombardSingleTarget.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}