using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Ledian : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ledian")
            .WithCardType("Enemy")
            .SetStats(3, 1, 4)
            .SetSprites(
                Mod.GetSprite("Ledian"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(6)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack("Shell", 5),
                    Mod.SStack("On Turn Apply Shell To Self", 2),
                    Mod.SStack(WhenShellLostGainEqualAttack.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}