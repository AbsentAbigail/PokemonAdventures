using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class Pokeball : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Pokeball")
            .SetDamage(1)
            .CanPlayOnEnemy()
            .CanPlayOnFriendly(false)
            .SetSprites(
                Mod.GetSprite("Pokeball"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Mod.TStack(Traits.Pokeball.Name),
                    Mod.TStack("Consume"),
                ];
                card.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Enemy"]),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}