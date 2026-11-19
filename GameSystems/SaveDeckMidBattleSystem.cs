namespace PokemonMod.GameSystems;

// Code by @semmiesem9
internal class SaveDeckMidBattleSystem : GameSystem
{
    private static SaveDeckMidBattleSystem _instance;

    public bool promptSave;

    public void OnEnable()
    {
        _instance = this;
        Events.OnBattleStateBuild += CheckSave;
    }

    public void OnDisable()
    {
        Events.OnBattleStateBuild -= CheckSave;
    }

    public static void PromptSave()
    {
        if (_instance != null)
        {
            _instance.promptSave = true;
        }
    }

    public void CheckSave()
    {
        if (!promptSave)
        {
            return;
        }

        Campaign.PromptSave();
        promptSave = false;
    }
}