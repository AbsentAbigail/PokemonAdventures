using System.Collections;
using PokemonMod.Builders.Cards.Items;

namespace PokemonMod.GameSystems;

// Code by semmiesem9
public class CustomRewardSystem : GameSystem
{
    public void OnEnable()
    {
        Events.OnBattleWinPreRewards += BattleWinPreRewards;
    }

    public void OnDisable()
    {
        Events.OnBattleWinPreRewards -= BattleWinPreRewards;
    }

    public void BattleWinPreRewards()
    {
        ActionQueue.Add(new ActionSequence(CustomSunVaultSequence()));
    }

    private static IEnumerator CustomSunVaultSequence()
    {
        var newRewards = GetRewards();

        if (newRewards == null)
        {
            yield break;
        }

        const string key = "rewards";
        var node = Campaign.FindCharacterNode(References.Battle.player);
        var oldRewards = node.data.GetValueOrDefault(key, null);

        node.data[key] = newRewards;

        yield return SceneManager.Load("BossReward", SceneType.Temporary);
        var sequence = FindObjectOfType<GainBlessingSequence2>();
        var sceneSprite = sequence.transform.GetChild(3).GetChild(1).GetComponent<ImageSprite>();
        sceneSprite.sprite = Mod.GetCard(Pokeball.Name).mainSprite;
        sceneSprite.preserveAspect = true;
        sequence.titleKey = Mod.GetLocalizedString("CharmReward");
        sequence.UpdateCinemaBarPrompt();
        yield return SceneManager.WaitUntilUnloaded("BossReward");

        node.data.Remove(key);

        // Return boss rewards
        if (oldRewards != null)
        {
            node.data[key] = oldRewards;
        }
    }

    private static CampaignNodeTypeBoss.RewardData GetRewards()
    {
        var characterNode = Campaign.FindCharacterNode(References.Player);
        return characterNode.data.Get<CampaignNodeTypeBoss.RewardData>("pokemon.rewards", null);
    }
}