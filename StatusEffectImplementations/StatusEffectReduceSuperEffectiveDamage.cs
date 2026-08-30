using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectReduceSuperEffectiveDamage : StatusEffectData
{
    public override void Init()
    {
        TypeInteractionSystem.OnSuperEffectiveEvent += Check;
    }

    private void OnDestroy()
    {
        TypeInteractionSystem.OnSuperEffectiveEvent -= Check;
    }

    private void Check(Hit hit)
    {
        if (hit.target != target)
        {
            return;
        }
        
        var amount = GetAmount();
        hit.damage -= amount;
        
        var localisedString = Mod.GetLocalizedString("KasibLog");
        FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Buff, BattleLogSystem.GetBattleEntity(target), amount);
    }
}