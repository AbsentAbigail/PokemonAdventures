using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Helpers;

namespace PokemonMod.Patches;

[HarmonyPatch(typeof(CampaignNodeTypeBattle), nameof(CampaignNodeTypeBattle.SetUp), typeof(CampaignNode))]
public class CampaignNodeTypeBattlePatches
{
    [UsedImplicitly]
    private static IEnumerator Postfix(IEnumerator __result, CampaignNode node)
    {
        yield return __result;
        
        LogHelper.Log("Postfix");
        if (!node.data.TryGetValue("battle", out var obj) || obj is not string assetName)
        {
            LogHelper.Log("no battle");
            yield break;
        }
        
        LogHelper.Log("battle " + assetName);
        var battleData = AddressableLoader.Get<BattleData>("BattleData", assetName);
        if (battleData == null)
        {
            LogHelper.Log("battle not found");
            yield break;
        }

        LogHelper.Log("found battle");
        var rewardList = GetCharmNamesFromFight(battleData);
            
        if (!rewardList.Any())
        {
            LogHelper.Log("no rewards");
            yield break;
        }
        LogHelper.Log("rewards " + rewardList.Count);
        
        var rewards = new CampaignNodeTypeBoss.RewardData
        {
            canTake = 1,
            rewards = rewardList
                .Select(reward => new BossRewardDataRandomCharm.Data
                {
                    type = BossRewardData.Type.Charm,
                    upgradeName = reward,
                })
                .Cast<BossRewardData.Data>().ToList(),
        };
        
        node.data ??= new Dictionary<string, object>();
        node.data.Add("pokemon.rewards", rewards);
    }

    private static List<string> GetCharmNamesFromFight(BattleData battleData)
    {
        List<string> rewardList = [];
        foreach (var wavePool in battleData.pools)
        {
            foreach (var wave in wavePool.waves)
            {
                foreach (var unit in wave.units)
                {
                    if (!unit.cardType.miniboss || unit.GetCustomDataOrNull("pokemon.rewards") is not SaveCollection<string> bossRewards)
                    {
                        continue;
                    }
                    
                    rewardList.AddRange(bossRewards.collection);
                    return rewardList;
                }
            }
        }
        
        return rewardList;
    }
}