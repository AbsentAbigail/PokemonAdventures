using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class BluePresent : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Blue Present")
            .SetDamage(5)
            .SetSprites(
                Mod.GetSprite("BluePresent"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Mod.TStack("Consume"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}