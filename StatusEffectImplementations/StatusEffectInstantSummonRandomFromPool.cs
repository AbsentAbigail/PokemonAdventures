using System.Collections;
using System.Collections.Generic;
using Extensions = Deadpan.Enums.Engine.Components.Modding.Extensions;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantSummonRandomFromPool : StatusEffectInstantSummon
{
    public string rewardPoolName;
    public CardData[] pool;

    public override IEnumerator Process()
    {
        Routine.Clump clump = new();
        var amount = GetAmount();
        for (var i = 0; i < amount; i++)
        {
            if (pool.Length > 0)
            {
                var card = GetPool().RandomItem().Clone();
                targetSummon.summonCard = card;
            }
            clump.Add(TrySummon());
            yield return clump.WaitForEnd();
        }

        yield return Remove();
    }

    private CardData[] GetPool()
    {
        var result = new List<CardData>(pool);
        if (rewardPoolName.IsNullOrEmpty() || Extensions.GetRewardPool(rewardPoolName) is not { } rewardPool)
        {
            return result.ToArray();
        }
        
        foreach (var dataFile in rewardPool.list)
        {
            if (dataFile is CardData cardData)
            {
                result.Add(cardData);
            }
        }

        return result.ToArray();
    }
}