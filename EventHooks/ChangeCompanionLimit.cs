using System.Threading.Tasks;

namespace PokemonMod.EventHooks;

public static class ChangeCompanionLimit
{
    public static Task CampaignGenerated()
    {
        if (References.PlayerData.classData.id != "pokemon")
        {
            return Task.CompletedTask;
        }
        
        References.PlayerData.companionLimit = 6;
        return Task.CompletedTask;
    }
}