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
            card.mentionedCards.Add(replacementCard);
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
}