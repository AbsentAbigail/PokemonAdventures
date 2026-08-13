using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectBossReward : StatusEffectApplyX
{
    public CardUpgradeData[] choices;
    
    public override void Init()
    {
        OnEntityDestroyed += CheckDestroy;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return entity == target;
    }

    private IEnumerator CheckDestroy(Entity entity, DeathType deathType)
    {
        if (entity.LastHitStillProcessing())
            yield return entity.WaitForLastHitToFinishProcessing();
        yield return FindObjectOfType<MiniBossRewardSystem>(true).ChooseRewards(choices);
    }
}