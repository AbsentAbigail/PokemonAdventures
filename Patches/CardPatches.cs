using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Patches;

[HarmonyPatch(typeof(Card), nameof(Card.SetDescription))]
public class CardPatches
{
    [UsedImplicitly]
    private static void Postfix(Card __instance)
    {
        ShowLeaderCompanions(__instance);
        StanceChange(__instance);
    }

    private static void ShowLeaderCompanions(Card card)
    {
        var customData = card.entity.data.GetCustomDataOrNull("pokemon.replacePreRun");
        if (customData is not SaveCollection<string> replaceWith)
        {
            return;
        }
        
        foreach (var replacement in replaceWith.collection)
        {
            var replacementCard = Mod.GetCard(replacement);
            if (replacementCard.cardType.miniboss)
            {
                continue;
            }
            if (!AddStanceChangePartner(card, replacementCard)) // If tag team, add both stances, otherwise just add the card
            {
                card.mentionedCards.Add(replacementCard);
            }
        }
    }

    private static void StanceChange(Card card)
    {
        var stanceChange = card.entity?.statusEffects.FirstOrDefault(status => status is StatusEffectStanceChange) as StatusEffectStanceChange;
        if (!stanceChange)
        {
            return;
        }

        var newCard = Mod.GetCard(card.entity.data.name).Clone();
        newCard.startWithEffects =
        [
            .. newCard.startWithEffects,
            .. !stanceChange.isFirstStance ? stanceChange.firstStance : stanceChange.secondStance,
        ];
        
        card.mentionedCards.Add(newCard);
    }

    private static bool AddStanceChangePartner(Card card, CardData partner)
    {
        var stanceTrait = partner.traits?.FirstOrDefault(trait => trait.data.effects?.Any(status => status is StatusEffectStanceChange) ?? false)?.data;
        if (stanceTrait == null)
        {
            return false;
        }
        var stanceChange = stanceTrait.effects.FirstOrDefault(status => status is StatusEffectStanceChange) as StatusEffectStanceChange;
        if (!stanceChange)
        {
            return false;
        }
        
        var stance1 = Mod.GetCard(partner.name).Clone();
        stance1.startWithEffects =
        [
            .. stance1.startWithEffects,
            .. stanceChange.firstStance,
        ];
        card.mentionedCards.Add(stance1);
        
        var stance2 = Mod.GetCard(partner.name).Clone();
        stance2.name += "2"; // Mentioned cards don't get added if they share a name, so change the name
        stance2.startWithEffects =
        [
            .. stance2.startWithEffects,
            .. stanceChange.secondStance,
        ];
        card.mentionedCards.Add(stance2);
        
        return true;
    }
}