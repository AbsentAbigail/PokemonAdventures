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

        GameObject.Find("Systems")?.AddComponent<TypeInteractionSystem>();
        GameObject.Find("Systems")?.AddComponent<CustomTextPopupSystem>();
        GameObject.Find("Systems")?.AddComponent<MiniBossRewardSystem>();
        GameObject.Find("Systems")?.AddComponent<ConsumedItemTrackerSystem>();
        GameObject.Find("Systems")?.AddComponent<TypePreviewSystem>();
        GameObject.Find("Systems")?.AddComponent<SaveDeckMidBattleSystem>();
        GameObject.Find("Systems")?.AddComponent<CustomRewardSystem>();
    }
}