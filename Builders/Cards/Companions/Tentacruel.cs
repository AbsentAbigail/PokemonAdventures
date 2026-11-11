using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Tentacruel : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Tentacruel")
            .SetStats(8, 2, 3)
            .SetSprites(
                Mod.GetSprite("Tentacruel"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(ConstrictApplyInk.Name),
                    Mod.SStack(WhenDebuffedConstrictApplierAndCleanseSelf.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}