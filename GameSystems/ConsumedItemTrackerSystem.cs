using System.Collections.Generic;

namespace PokemonMod.GameSystems;

public class ConsumedItemTrackerSystem : GameSystem
{
    public HashSet<string> ConsumedCards = [];

    public void OnEnable()
    {
        Events.OnBattleStart += BattleStart;
        Events.OnBattleLoaded += BattleLoaded;
        Events.OnActionQueued += ActionQueued;
    }

    public void OnDisable()
    {
        Events.OnBattleStart -= BattleStart;
        Events.OnBattleLoaded -= BattleLoaded;
        Events.OnActionQueued -= ActionQueued;
    }

    public void BattleStart()
    {
        ConsumedCards = [];
        SaveData();
    }

    public void ActionQueued(PlayAction action)
    {
        if (action is not ActionConsume actionConsume || actionConsume.blocked)
        {
            return;
        }
        
        ConsumedCards.Add(actionConsume.target.name);
        SaveData();
    }

    public void BattleLoaded() => LoadData();

    public void SaveData()
    {
        var id = Campaign.FindCharacterNode(References.Battle.player).id;
        SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "PokemonConsumeItemTrackerBattleId", id);
        SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "PokemonConsumeItemTracker", new SaveCollection<string>(ConsumedCards.ToArray()));
    }

    public void LoadData()
    {
        var id = Campaign.FindCharacterNode(References.Battle.player).id;
        if (SaveSystem.LoadCampaignData(Campaign.Data.GameMode, "PokemonConsumeItemTrackerBattleId", -1) != id)
        {
            return;
        }
        ConsumedCards = [.. SaveSystem.LoadCampaignData(Campaign.Data.GameMode, "PokemonConsumeItemTracker", new SaveCollection<string>()).collection];
    }
}