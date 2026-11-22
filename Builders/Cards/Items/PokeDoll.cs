using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class PokeDoll : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "PokeDoll")
            .SetDamage(3)
            .SetSprites(
                Mod.GetSprite("PokeDoll"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .WithValue(50)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(InstantSummonSubstitute.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Consume"),
                ];

                // Extra Target Constraint because the item has an attack stat
                card.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Friendly", "Enemy"]),
                    TargetConstraintHelper.IsPokemon(),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}