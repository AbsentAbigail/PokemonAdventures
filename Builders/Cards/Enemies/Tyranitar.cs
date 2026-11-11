using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Tyranitar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Tyranitar")
            .WithCardType("Enemy")
            .SetStats(9, 9, 5)
            .SetSprites(
                Mod.GetSprite("Tyranitar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sandstorm))
            .DropsBling(7)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Dark.Name),
                    Mod.SStack(CastSandstorm.Name, 2),
                    Mod.SStack(WhenHitGainShellEqualToSandstorm.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}