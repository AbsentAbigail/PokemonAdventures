using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class PinkPresent : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Pink Present")
            .SetDamage(null)
            .SetSprites(
                Mod.GetSprite("PinkPresent"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Heal", 5),
                ];
                card.traits =
                [
                    Mod.TStack("Consume"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}