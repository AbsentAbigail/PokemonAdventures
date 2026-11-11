using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Larvitar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Larvitar")
            .WithCardType("Enemy")
            .SetStats(5, 5, 5)
            .SetSprites(
                Mod.GetSprite("Larvitar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sandstorm))
            .DropsBling(4)
            .EvolvesInto(Pupitar.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(CastSandstorm.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}