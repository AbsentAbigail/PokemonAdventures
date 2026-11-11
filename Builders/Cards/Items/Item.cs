using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class Item : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Item")
            .SetSprites(
                Mod.GetSprite("Item"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
            })
            .WithText($"<color=#{KeywordColours.Gray.ToHexRGB()}>Gain the equipped charm:</color>\n{{0}}");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}