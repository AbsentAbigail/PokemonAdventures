using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableImages;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class EeveeAndEevee : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Eevee and Eevee")
            .SetStats(4, 1, 4)
            .SetSprites(
                Mod.GetSprite("EeveeAndEevee1"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .EvolvesInto(UmbreonAndEspeon.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack("MultiHit"),
                ];
                card.traits =
                [
                    Mod.TStack(TagTeamEeveeAndEevee.Name),
                ];

                var scriptableCardImage = Mod.CreateScriptableCardImage<TagTeamImage>("Tag Team Eevee and Eevee Image");
                scriptableCardImage.sprite1 = Mod.GetSprite("EeveeAndEevee1");
                scriptableCardImage.sprite2 = Mod.GetSprite("EeveeAndEevee2");
                card.scriptableImagePrefab = scriptableCardImage;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}