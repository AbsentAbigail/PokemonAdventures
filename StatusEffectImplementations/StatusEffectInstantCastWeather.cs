using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Deadpan.Enums.Engine.Components.Modding;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantCastWeather : StatusEffectInstantSummon
{
    public StatusEffectData increaseHealthEffect;
    public StatusEffectData intensityEffect;
    public int healthIncreaseBase = 5;

    public override IEnumerator Process()
    {
        var weather = Battle.GetCardsOnBoard().FirstOrDefault(ally => ally.data.name == targetSummon.summonCard.name);
        
        if (!weather)
        {
            yield return NewWeather();
            yield return Remove();
            yield break;
        }
        
        var intensity = weather.statusEffects.FirstOrDefault(status => status.name == intensityEffect.name)?.count ?? 0;

        var hit = new Hit(applier, weather, 0);
        hit.AddStatusEffect(intensityEffect, GetAmount());
        hit.AddStatusEffect(increaseHealthEffect, healthIncreaseBase - intensity);
        yield return hit.Process();
        weather.display.promptUpdateDescription = true;
        weather.PromptUpdate();
        
        yield return Remove();
    }

    private IEnumerator NewWeather()
    {
        if (targetSummon.toSummon == null)
        {
            var list = new HashSet<CardContainer>();
            list.AddRange(target.actualContainers);
            if (list.Count > 0 && list.ToArray().RandomItem() is CardSlot cardSlot)
            {
                targetSummon.toSummon = [
                    cardSlot,
                ];
            }
        }
        
        var originalData = targetSummon.summonCard;
        targetSummon.summonCard = targetSummon.summonCard.Clone();
        var summonCard = targetSummon.summonCard;
        var intensity = summonCard.startWithEffects.FirstOrDefault(status => status.data.name == intensityEffect.name);

        if (intensity != null)
        {
            intensity.count = GetAmount();
        }
        else
        {
            summonCard.startWithEffects =
            [
                .. summonCard.startWithEffects,
                new CardData.StatusEffectStacks(intensityEffect, GetAmount()),
            ];
        }
        
        var controller = target.display.hover.controller;
        target.curveAnimator.Ping();
        var cardSlotArray = targetSummon.toSummon;
            
        for (var index = 0; index < (cardSlotArray?.Length ?? 0); ++index)
        {
            yield return TrySummon(cardSlotArray![index], controller, target);
        }
        
        targetSummon.summonCard = originalData;
    }

    private IEnumerator TrySummon(
        CardContainer container,
        CardController controller,
        Entity summonedBy) {
        
        if (container.Count < container.max)
        {
            yield return targetSummon.Summon(container, controller, summonedBy);
        }
        else
        {
            if (ShoveSystem.CanShove(container.GetTop(), target, out var shoveData))
            {
                yield return ShoveSystem.DoShove(shoveData, true);
                yield return targetSummon.Summon(container, controller, summonedBy);
            }
            else if (NoTargetTextSystem.Exists())
            {
                yield return NoTargetTextSystem.Run(target, NoTargetType.NoSpaceToSummon);
            }
        }
    }
}