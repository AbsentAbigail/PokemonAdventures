using Deadpan.Enums.Engine.Components.Modding;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Patches;
using UnityEngine;
using UnityEngine.Localization.Tables;

namespace PokemonMod.Variables;

public static class Localization
{
    public static void CreateLocalizedStrings()
    {
        var uiText = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English);
        
        AddStatusText(uiText);
        
        uiText.SetString(InstantChooseEvolution.Name, "Do you want to evolve {0}?");
        
        uiText.SetString("MinibossReward", "You defeated a miniboss! Pick your reward");
        
        uiText.SetString(CustomStats.PokemonCaughtKey, "Pokemon caught: {0}");
        uiText.SetString(CustomStats.PokemonEvolvedKey, "Pokemon evolved: {0}");
        
        uiText.SetString("CountUpWaveBell", "{0} counted up the Wave Bell by [{1}]");
        
        uiText.SetString("TagTeamEevee1", "Eevee");
        uiText.SetString("TagTeamEevee2", "Eevee");
    }

    private static void AddStatusText(StringTable uiText)
    {
        uiText.SetString("SuperEffectiveLog", "{1} is weak to {0}, damage is increased by {2}");
        uiText.SetString("ResistedLog", "{1} resists {0}, damage is decreased by {2}");
        uiText.SetString("ImmuneLog", "{1} is immune against {0}, damage is decreased by {2}");

        uiText.SetString("CaughtPokemon", "Yay! You caught {0}!");
        uiText.SetString("Sleep", "{0} is asleep");
        uiText.SetString("Paralysed", "{0} is paralysed!");
        uiText.SetString("ConstrictedFreed", "{0} freed itself of Constrict!");
    }
}