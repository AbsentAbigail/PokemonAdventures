using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class LeppaBerry : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Leppa Berry")
            .SetDamage(null)
            .CanPlayOnHand()
            .SetSprites(
                Mod.GetSprite("LeppaBerry"),
                Mod.GetBackgroundSprite(BackgroundSprites.Berry))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Reduce Counter", 2),
                ];
                card.traits =
                [
                    Mod.TStack("Zoomlin"),
                ];
            })
            .IsBerry();
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}