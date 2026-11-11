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
        uiText.SetString(OnCardPlayedRestoreConsumedCard.Name, "Which card do you want to recycle?");
        
        uiText.SetString("LinkCable", "Which Pokemon do you want to trade for?");
        
        uiText.SetString("MinibossReward", "You defeated a miniboss! Pick your reward");
        uiText.SetString("BossReward", "You defeated a boss! Here's your reward");
        
        uiText.SetString(CustomStats.PokemonCaughtKey, "Pokemon caught: {0}");
        uiText.SetString(CustomStats.PokemonEvolvedKey, "Pokemon evolved: {0}");
        
        uiText.SetString("CountUpWaveBell", "{0} counted up the Wave Bell by [{1}]");
        
        uiText.SetString("TagTeamEevee1", "Eevee");
        uiText.SetString("TagTeamEevee2", "Eevee");
        
        uiText.SetString("CorsolaDies", ":(");
    }

    private static void AddStatusText(StringTable uiText)
    {
        uiText.SetString("SuperEffectiveLog", "<sprite name={1}> is weak to <sprite name={0}>, damage is increased by [{2}]");
        uiText.SetString("ResistedLog", "<sprite name={1}> resists <sprite name={0}>, damage is decreased by [{2}]");
        uiText.SetString("ImmuneLog", "<sprite name={1}> is immune against <sprite name={0}>, damage is decreased by [{2}]");
        uiText.SetString("KasibLog", "{0} takes [{1}] less damage from super effective attacks");

        uiText.SetString("CaughtPokemon", "Yay! You caught {0}!");
        uiText.SetString("Sleep", "{0} is asleep");
        uiText.SetString("Paralysed", "{0} is paralysed!");
        uiText.SetString("ConstrictedFreed", "{0} freed itself of Constrict!");
    }
}