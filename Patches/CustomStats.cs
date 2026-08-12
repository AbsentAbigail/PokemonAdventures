using System.Collections.Generic;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Helpers;

namespace PokemonMod.Patches;

[HarmonyPatch]
public class CustomStats
{
    private static bool _loaded = false;
    private static readonly List<GameStatData> CustomStatData = [];
    
    public static string PokemonCaughtKey = "StatPokemonCaught";
    public static string PokemonEvolvedKey = "StatPokemonEvolved";

    public static void AddPokemonCaught()
    {
        Add(PokemonCaughtKey, 1);
    }
    
    public static void AddPokemonEvolved()
    {
        Add(PokemonEvolvedKey, 1);
    }

    private static void Add(string key, int amount)
    {
        StatsSystem.instance.stats.Add(key, amount);
    }
    
    [UsedImplicitly, HarmonyPrefix, HarmonyPatch(typeof(StatsPanel), nameof(StatsPanel.Awake))]
    private static void Prefix(StatsPanel __instance)
    {
        if (!_loaded)
        {
            _loaded = true;
            LoadCustomStats();
        }

        __instance.stats =
        [
            ..__instance.stats,
            .. CustomStatData,
        ];
    }

    private static void LoadCustomStats()
    {
        CreateStat(PokemonCaughtKey, 100f);
        CreateStat(PokemonEvolvedKey, 100f);
    }

    private static void CreateStat(string name, float priority = 0f, float par = 1f, float priorityAddOverPar = 0f,
        float prioritySubUnderPar = 0f)
    {
        GameStatData stat = new Script<GameStatData>();
        stat.name = name;
        stat.type = GameStatData.Type.Count;
        stat.statName = name;
        stat.stringKey = Mod.GetLocalizedString(name);
        stat.priority = priority;
        stat.par = par;
        stat.priorityAddOverPar = priorityAddOverPar;
        stat.prioritySubUnderPar = prioritySubUnderPar;
        CustomStatData.Add(stat);
    }
}