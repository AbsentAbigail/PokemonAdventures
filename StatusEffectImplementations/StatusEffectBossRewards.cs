using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectBossReward : StatusEffectInstant
{
    public CardUpgradeData[] choices;
    
    public override IEnumerator Process()
    {
        yield return FindObjectOfType<MiniBossRewardSystem>(true).ChooseRewards(choices);
        yield return Remove();
    }
}