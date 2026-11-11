using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class AssaultVest : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Assault Vest")
            .SetDamage(null)
            .CanPlayOnHand()
            .SetSprites(
                Mod.GetSprite("AssaultVest"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .WithValue(50)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Shell", 7),
                    Mod.SStack("Null", 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}