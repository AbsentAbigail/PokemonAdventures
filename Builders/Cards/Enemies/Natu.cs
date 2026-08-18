using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;
using BombardSingleTarget = PokemonMod.Builders.Traits.BombardSingleTarget;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Natu : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Natu")
            .WithCardType("Enemy")
            .SetStats(4, 2, 3)
            .SetSprites(
                Mod.GetSprite("Natu"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(5)
            .EvolvesInto(Xatu.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(DealAdditionalDamage.Name, 4),
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