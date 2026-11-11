using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectAcrobatics : StatusEffectWhileActiveX
{
    public override void Init()
    {
        base.Init();
        OnCardPlayed += Refresh;
        OnCardMove += Refresh;
    }

    private IEnumerator Refresh(Entity entity)
    {
        if (!active || GetAmount(target) == currentAmount)
        {
            yield break;
        }
        ActionQueue.Add(new ActionRefreshWhileActiveEffect(this));
    }

    private IEnumerator Refresh(Entity entity, Entity[] targets)
    {
        return Refresh(null);
    }

    public override int GetAmount(Entity entity, bool equalAmount = false, int equalTo = 0)
    {
        if (!target || TargetSilenced())
        {
            return 0;
        }
        if (scriptableAmount)
        {
            return scriptableAmount.Get(entity) * GetAmount();
        }
        return !equalAmount ? GetAmount() : equalTo;
    }
}