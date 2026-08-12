using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Cards.Items;

[UsedImplicitly]
public class BulletPunch : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateItem(Name, "Bullet Punch")
            .SetDamage(0)
            .SetSprites(
                Mod.GetSprite("BulletPunch"),
                Mod.GetBackgroundSprite(BackgroundSprites.Item))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Mod.TStack("Consume"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}