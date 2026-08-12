using UnityEngine;

namespace PokemonMod.Helpers;

public static class LogHelper
{
    public static void Log(object message)
    {
        Debug.Log($"[Pokemon] {message}");
    }

    public static void Warn(object message)
    {
        Debug.LogWarning($"[Pokemon Warning] {message}");
    }

    public static void Error(object message)
    {
        Debug.LogError($"[Pokemon Error] {message}");
    }
}