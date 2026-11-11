using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Pupitar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Pupitar")
            .WithCardType("Enemy")
            .SetStats(7, 7, 5)
            .SetSprites(
                Mod.GetSprite("Pupitar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sandstorm))
            .DropsBling(5)
            .EvolvesInto(Tyranitar.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack(CastSandstorm.Name),
                    Mod.SStack(WhenHitGainShellEqualToSandstorm.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}