using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.GameSystems;

namespace PokemonMod.Patches;

[HarmonyPatch(typeof(Hit), nameof(Hit.Process))]
public class HitPatches
{
    [UsedImplicitly]
    private static void Prefix(Hit __instance)
    {
        TypeInteractionSystem.Check(__instance);
    }
}