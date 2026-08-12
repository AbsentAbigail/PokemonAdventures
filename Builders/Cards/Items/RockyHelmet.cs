using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class RockyHelmet : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Rocky Helmet")
            .SetDamage(null)
            .CanPlayOnHand()
            .SetSprites(
                Mod.GetSprite("RockyHelmet"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .WithValue(50)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Teeth", 3),
                ];
                card.traits =
                [
                    Mod.TStack("Consume"),
                ];
                card.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCanBeHit>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}