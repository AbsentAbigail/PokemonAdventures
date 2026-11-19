using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class ExeggutorAlola : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Exeggutor")
            .WithCardType("Enemy")
            .SetStats(8, 3, 5)
            .SetSprites(
                Mod.GetSprite("ExeggutorAlola"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Dragon.Name),
                    Mod.SStack(DealAdditionalDamageEqualToBerriesInHandBoostable.Name, 2),
                ];
                card.traits =
                [
                    Mod.TStack("Barrage"),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}