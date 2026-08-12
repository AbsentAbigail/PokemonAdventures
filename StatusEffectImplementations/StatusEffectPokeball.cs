using System.Collections;
using PokemonMod.GameSystems;
using PokemonMod.Patches;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectPokeball : StatusEffectData
{
    public override void Init()
    {
        OnEntityDestroyed += CheckDestroy;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return entity.data.cardType == Mod.GetCardType("Enemy") && entity.lastHit != null && entity.lastHit.attacker == target && entity.owner != target.owner;
    }

    private static IEnumerator CheckDestroy(Entity entity, DeathType deathType)
    {
        var card = entity.data.Clone();
        card.cardType = Mod.GetCardType("Friendly");
        References.PlayerData.inventory.reserve.Add(card);
        yield return CustomTextPopupSystem.RunNoWait(entity, Mod.GetLocalizedString("CaughtPokemon"), card.title);
        CustomStats.AddPokemonCaught();
        Campaign.PromptSave();
    }
}