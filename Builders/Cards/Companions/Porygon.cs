using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Porygon : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Porygon")
            .SetStats(7, 3, 3)
            .SetSprites(
                Mod.GetSprite("Porygon"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pokeball))
            .DropsBling(7)
            .EvolvesInto(Porygon2.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                ];
                card.traits = [
                    Mod.TStack(Download.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}