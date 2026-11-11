using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Growlithe : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Growlithe")
            .SetStats(5, 4, 4)
            .SetSprites(
                Mod.GetSprite("Growlithe"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(4)
            .WithText("I don't evolve yet :(")
            //.EvolvesInto(Haunter.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(WhileActiveDecreaseAttackOfEnemies.Name),
                    Mod.SStack("On Card Played Damage To Self"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}