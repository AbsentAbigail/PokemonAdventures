using System.Collections;
using System.Linq;
using PokemonMod.Helpers;
using UnityEngine;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectSubstitute : StatusEffectData
{
    private const string Key = "substitute";
    private static CardContainer ReserveContainer => References.Player.reserveContainer;

    public override void Init()
    {
        OnEntityDestroyed += Destroy;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return target == entity && target.data.customData.ContainsKey(Key);
    }

    private IEnumerator Destroy(Entity entity, DeathType deathType)
    {
        if (target.data.GetCustomDataOrNull(Key) is not ulong cardId)
        {
            yield break;
        }
        var card = ReserveContainer.FirstOrDefault(card => card.data.id == cardId);
        if (!card)
        {
            yield break;
        }

        var localisedString = Mod.GetLocalizedString("BreakSubstitute");
        FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Debuff, BattleLogSystem.GetBattleEntity(target));

        const float animationTime = 0.4f;
        card.transform.position = entity.transform.position;
        card.transform.localScale = Vector2.zero;
        LeanTween.scale(card.gameObject, entity.preContainers[0].GetChildScale(target), animationTime).setEaseOutBack();
        card.flipper.FlipUpInstant();
        card.wobbler.WobbleRandom(2f);
        card.curveAnimator.Ping();
        yield return Sequences.Wait(animationTime + 0.2f);
        yield return Sequences.CardMove(card, entity.preContainers);
        yield return new ActionRunEnableEvent(card).Run();
    }
}