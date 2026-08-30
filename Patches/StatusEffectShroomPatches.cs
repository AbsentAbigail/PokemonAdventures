using HarmonyLib;
using JetBrains.Annotations;

namespace PokemonMod.Patches;

[HarmonyPatch(typeof(StatusEffectShroom), nameof(StatusEffectShroom.RunTurnEndEvent), typeof(Entity))]
public class StatusEffectShroomPatches
{
    [UsedImplicitly]
    private static bool Postfix(bool __result, StatusEffectShroom __instance, Entity entity)
    {
        return __result && Battle.IsOnBoard(__instance.target);
    }
}