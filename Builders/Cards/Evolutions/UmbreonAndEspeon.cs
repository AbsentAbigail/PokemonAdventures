using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableImages;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class UmbreonAndEspeon : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Umbreon and Espeon")
            .SetStats(10, 1, 5)
            .SetSprites(
                Mod.GetSprite("UmbreonAndEspeon1"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("MultiHit"),
                ];
                card.traits =
                [
                    Mod.TStack(TagTeamUmbreonAndEspeon.Name),
                ];

                var scriptableCardImage = Mod.CreateScriptableCardImage<TagTeamImage>("Tag Team Umbreon and Espeon Image");
                scriptableCardImage.sprite1 = Mod.GetSprite("UmbreonAndEspeon1");
                scriptableCardImage.sprite2 = Mod.GetSprite("UmbreonAndEspeon2");
                card.scriptableImagePrefab = scriptableCardImage;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}