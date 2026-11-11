using System.Collections.Generic;
using System.Linq;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Clunkers;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using Extensions = Deadpan.Enums.Engine.Components.Modding.Extensions;
using Object = UnityEngine.Object;

namespace PokemonMod.Builders.Tribes;

[UsedImplicitly]
public class PokemonTribe : IClassBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public const string UnitPoolName = "PokemonUnitPool";
    private static readonly List<string> UnitPoolCards = [];
    public const string ItemPoolName = "PokemonItemPool";
    private static readonly List<string> ItemPoolCards = [];
    public const string BerryPoolName = "PokemonBerryPool";
    private static readonly List<string> BerryPoolCards = [];
    public const string CharmPoolName = "PokemonCharmPool";
    private static readonly List<string> CharmPoolCharms = [];
    
    public static readonly string TitleKey = Mod.PrefixGuid("TribeTitle");
    public static readonly string DescKey = Mod.PrefixGuid("TribeDesc");
    
    public DataFileBuilder<ClassData, ClassDataBuilder> Builder()
    {
        return Mod.TribeCopy("Basic", Name)
            .WithFlag(Mod.GetSprite("tribebanner"))
            .SubscribeToAfterAllBuildEvent(tribe =>
            {
                tribe.id = "pokemon";
                
                var playerCharacter = tribe.characterPrefab.gameObject.InstantiateKeepName();
                Object.DontDestroyOnLoad(playerCharacter);
                playerCharacter.name = "Pokemon";
                tribe.characterPrefab = playerCharacter.GetComponent<Character>();
                tribe.characterPrefab.data.companionLimit = 6;
                Inventory inventory = new Script<Inventory>("Inventory (Pokemon)", null);
                inventory.deck.list = DataList<CardData>(
                    MudBall.Name, MudBall.Name, MudBall.Name,
                    AuxPowerguard.Name, AuxPowerguard.Name,
                    Potion.Name,
                    RareCandy.Name, Pokeball.Name, Recycle.Name,
                    BerryBush.Name
                ).ToList();
                tribe.startingInventory = inventory;

                tribe.leaders = DataList<CardData>([.. Pokemon.LeaderNames]);

                tribe.rewardPools =
                [
                    UnitPool(),
                    ItemPool(),
                    BerryPool(),
                    CharmPool(),
                    Extensions.GetRewardPool("GeneralModifierPool"),
                    Extensions.GetRewardPool("GeneralCharmPool"),
                ];
            });
    }

    public static void AddToUnitPool(string name)
    {
        UnitPoolCards.Add(name);
    }
    
    public static void AddToItemPool(string name)
    {
        ItemPoolCards.Add(name);
    }

    public static void AddToBerryPool(string name)
    {
        BerryPoolCards.Add(name);
    }

    public static void AddToCharmPool(string name)
    {
        CharmPoolCharms.Add(name);
    }

    private RewardPool UnitPool()
    {
        return CreateRewardPool(UnitPoolName, nameof(RewardPool.Type.Units),
        [
            .. DataList<CardData>([
                    .. UnitPoolCards,
                ]
            ),
        ]);
    }

    private static RewardPool ItemPool()
    {
        return CreateRewardPool(ItemPoolName, nameof(RewardPool.Type.Items),
        [
            .. DataList<CardData>(
                [
                    .. ItemPoolCards,
                    "PomDispenser", // Gacha Pomper
                    "Heartforge",
                    "MobileCampfire",
                    "PepperFlag",
                    "SpiceSparklers",
                    "Madness", // Sunglass Chime
                    "ZoomlinNest",
                    "BeepopMask",
                    "Bumblebee", // Blaze Bom
                    "Shwooper", // Blizard Bottle
                    "EnergyDart", // Clockwork Bom
                    "DragonflamePepper",
                    "FallowMask",
                    "Recycler", // Forging Stove
                    "Junberry", // Gigis Cookie Box
                    "JunjunMask",
                    "LuminShard", // Lumin Lantern
                    "NutshellCake",
                    "Peppereaper",
                    "Peppering",
                    "Putty", // Shade clay
                    "ShellShield",
                    "Shellbo",
                    "SpiceStones",
                ]
            ),
        ]);
    }

    private static RewardPool BerryPool()
    {
        return CreateRewardPool(BerryPoolName, nameof(RewardPool.Type.Items),
        [
            .. DataList<CardData>(
                [
                    .. BerryPoolCards,
                ]
            ),
        ]);
    }

    private static RewardPool CharmPool()
    {
        return CreateRewardPool(CharmPoolName, nameof(RewardPool.Type.Charms),
        [
            .. DataList<CardUpgradeData>(
            [
                    ..CharmPoolCharms,
                    "CardUpgradeAcorn",
                    "CardUpgradeSpiky",
                    "CardUpgradeBom",
                    "CardUpgradeConsumeOverload",
                    "CardUpgradeOverload",
                    "CardUpgradeTrash",
                    "CardUpgradeHeartburn",
                    "CardUpgradeScrap",
                    "CardUpgradeShellOnKill",
                    "CardUpgradeSpice",
                    "CardUpgradeTeethWhenHit",
                ]
            ),
        ]);
    }

    private static T[] DataList<T>(params string[] names) where T : DataFile
    {
        return names.Select(Mod.TryGet<T>).ToArray();
    }

    private static RewardPool CreateRewardPool(string name, string type, DataFile[] list)
    {
        RewardPool pool = new Script<RewardPool>();
        pool.name = name;
        pool.type = type;
        pool.list = list.ToList();
        return pool;
    }
}