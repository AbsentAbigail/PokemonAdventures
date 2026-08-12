using PokemonMod.GameSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonMod.EventHooks;

public static class AddGameSystems
{
    public static void SceneLoaded(Scene scene)
    {
        if (scene.name != "Campaign")
        {
            return;
        }

        GameObject.Find("Systems")?.AddComponent<CustomTextPopupSystem>();
        GameObject.Find("Systems")?.AddComponent<MiniBossRewardSystem>();
    }
}