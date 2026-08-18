using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectCastWeather : StatusEffectSummon
{
    public StatusEffectData increaseHealthEffect;
    public StatusEffectData intensityEffect;
    public int healthIncreaseBase = 5;
    
    public override void Init()
    {
        OnCardPlayed += CardPlayed;
    }

    private new IEnumerator CardPlayed(Entity entity, Entity[] targets)
    {
        if (toSummon == null)
        {
            var list = new HashSet<CardContainer>();
            list.AddRange(entity.actualContainers);
            if (list.Count > 0 && list.ToArray().RandomItem() is CardSlot cardSlot)
            {
                toSummon = [
                    cardSlot,
                ];
            }
        }
        
        var weather = Battle.GetCardsOnBoard().FirstOrDefault(ally => ally.data.name == summonCard.name);
        
        if (!weather)
        {
            yield return NewWeather();
            yield break;
        }
        
        var intensity = weather.statusEffects.FirstOrDefault(status => status.name == intensityEffect.name)?.count ?? 0;

        var hit = new Hit(applier, weather, 0);
        hit.AddStatusEffect(intensityEffect, GetAmount());
        hit.AddStatusEffect(increaseHealthEffect, healthIncreaseBase - intensity);
        yield return hit.Process();
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }

    private IEnumerator NewWeather()
    {
        var data = summonCard.Clone();
        var intensity = data.startWithEffects.FirstOrDefault(status => status.data.name == intensityEffect.name);

        if (intensity != null)
        {
            intensity.count = GetAmount();
        }
        else
        {
            data.startWithEffects =
            [
                .. data.startWithEffects,
                new CardData.StatusEffectStacks(intensityEffect, GetAmount()),
            ];
        }
        
        var controller = target.display.hover.controller;
        target.curveAnimator.Ping();
        var cardSlotArray = toSummon;
            
        for (var index = 0; index < (cardSlotArray?.Length ?? 0); ++index)
        {
            yield return TrySummon(cardSlotArray![index], controller, target);
        }
    }
}