using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Battles;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Tribes;
using PokemonMod.EventHooks;
using PokemonMod.Helpers;
using PokemonMod.Patches;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.U2D;
using UnityEngine.UI;
using WildfrostHopeMod.SFX;
using WildfrostHopeMod.Utils;
using WildfrostHopeMod.VFX;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Types = PokemonMod.Variables.Types;

namespace PokemonMod;

[UsedImplicitly]
public class Pokemon : WildfrostMod
{
    public static Pokemon Instance;

    public Pokemon(string directory) : base(directory)
    {
        Instance = this;
    }

    public override string GUID => "abigail.josephine.pelli.pokemon";

    public override string[] Depends => [
        "mhcdc9.wildfrost.battle",
        "hope.wildfrost.vfx",
        "hope.wildfrost.console"// todo: remove :(
    ];

    public override string Title => "Pokemon Adventures";

    public override string Description => "todo";

    private List<object> _assets;
    private bool _loaded = false;

    public static List<string> CharmNames = [];
    public static List<string> CompanionNames = [];
    public static List<string> ItemNames = [];
    public static List<string> ClunkerNames = [];
    public static List<string> LeaderNames = [];

    [UsedImplicitly]
    [ConfigItem(false, null, "Enable beta arts (Requires restart)")]
    public bool BetaArt;
    
    //this is here to allow our icon to appear in the text box of cards
    public override TMP_SpriteAsset SpriteAsset => _assetSprites;
    private TMP_SpriteAsset _assetSprites;

    // Change "Windows" to whatever you want it named
    // This is where the addressables will be stored
    public static string CatalogFolder => Path.Combine(Instance.ModDirectory, "Addressables");

    // A helpful shortcut
    public static string CatalogPath => Path.Combine(CatalogFolder, "catalog.json");

    public static SpriteAtlas SpriteAtlas;
    public static SpriteAtlas BetaSpriteAtlas;
    public static SpriteAtlas BackgroundSpriteAtlas;
    public static SpriteAtlas CharmSpriteAtlas;
    public static SpriteAtlas IconSpriteAtlas;

    public override void Load()
    {
        StopWatch.Start();
        
        if (!Addressables.ResourceLocators.Any(r => r is ResourceLocationMap map && map.LocatorId == CatalogPath))
        {
            Addressables.LoadContentCatalogAsync(CatalogPath).WaitForCompletion();
        }

        SpriteAtlas = GetAsset<SpriteAtlas>($"Assets/{GUID}/sprites.spriteatlas");
        BetaSpriteAtlas = GetAsset<SpriteAtlas>($"Assets/{GUID}/beta.spriteatlas");
        BackgroundSpriteAtlas = GetAsset<SpriteAtlas>($"Assets/{GUID}/backgrounds.spriteatlas");
        IconSpriteAtlas = GetAsset<SpriteAtlas>($"Assets/{GUID}/icon.spriteatlas");
        CharmSpriteAtlas = GetAsset<SpriteAtlas>($"Assets/{GUID}/charm.spriteatlas");

        VFXHelper.VFX = new GIFLoader(this, ImagePath("Anim"));
        VFXHelper.VFX.RegisterAllAsApplyEffect();

        VFXHelper.SFX = new SFXLoader(ImagePath("Sounds"));
        VFXHelper.SFX.RegisterAllSoundsToGlobal();


        //Needed to get sprites in text boxes
        _assetSprites = HopeUtils.CreateSpriteAsset("PokemonAssets", ImagePath("Icons"));
        SpriteAsset.RegisterSpriteAsset();

        if (!_loaded)
        {
            CreateModAssets();
        }
        base.Load();

        // Fight 1
        YoungsterFight.AddBattle();
        
        // Fight 3
        MistyGym.AddBattle();
        
        Evolutions.Setup();
            
        LoadEvents();

        var gameMode = Mod.TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
        gameMode.classes = gameMode.classes.Append(Mod.TryGet<ClassData>(PokemonTribe.Name)).ToArray();
        
        LogHelper.Log($"Finished loading in {StopWatch.Stop()} milliseconds");
    }

    public override void Unload()
    {
        UnloadEvents();
        UnloadFromClasses();
        base.Unload();

        var gameMode = Mod.TryGet<GameMode>("GameModeNormal");
        gameMode.classes = RemoveNulls(gameMode.classes);
        UnloadFromClasses();
    }

    private static void LoadEvents()
    {
        Events.OnSceneLoaded += AddGameSystems.SceneLoaded;
        Events.OnEntityCreated += FixLeaderImage;
        Events.OnCampaignGenerated += ChangeCompanionLimit.CampaignGenerated;
        Events.OnPreCampaignPopulate += ReplaceLeaders.PreCampaignPopulate;
    }

    private static void UnloadEvents()
    {
        Events.OnSceneLoaded -= AddGameSystems.SceneLoaded;
        Events.OnEntityCreated -= FixLeaderImage;
        Events.OnCampaignGenerated -= ChangeCompanionLimit.CampaignGenerated;
        Events.OnPreCampaignPopulate -= ReplaceLeaders.PreCampaignPopulate;
    }

    private static void FixLeaderImage(Entity entity)
    {
        if (entity.display is Card { hasScriptableImage: false } card)
        {
            card.mainImage.gameObject.SetActive(true);
        }
    }

    private void CreateModAssets()
    {
        _assets = [];

        const string builderNamespace = "PokemonMod.Builders";

        RegisterTypes();
            
        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Icons",
                    StringComparison.Ordinal)
                && typeof(IIconBuilder).IsAssignableFrom(t))
            .Select(type => ((IIconBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".StatusEffects",
                    StringComparison.Ordinal)
                && typeof(IStatusBuilder).IsAssignableFrom(t))
            .Select(type => ((IStatusBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Keywords",
                    StringComparison.Ordinal)
                && typeof(IKeywordBuilder).IsAssignableFrom(t))
            .Select(type => ((IKeywordBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Traits",
                    StringComparison.Ordinal)
                && typeof(ITraitBuilder).IsAssignableFrom(t))
            .Select(type => ((ITraitBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        var pets = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Pets",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(pets);

        var companions = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Companions",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(companions);

        var fieldEffects = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.FieldEffects",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(fieldEffects);

        var leaderPokemon = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.LeaderPokemon",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(leaderPokemon);

        var evolutions = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Evolutions",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(evolutions);

        var legendaries = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Legendaries",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(legendaries);

        var clunkers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Clunkers",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(clunkers);
        ClunkerNames = GetNamesFromBuilders(clunkers);

        var items = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Items",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(items);
        ItemNames = GetNamesFromBuilders(items);

        var leaderTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
            string.Equals(t.Namespace, builderNamespace + ".Cards.Leaders",
                StringComparison.Ordinal)
            && typeof(ITrainerBuilder).IsAssignableFrom(t)).ToArray();
        
        var leaders = leaderTypes.Select(type =>
            {
                var builder = (ITrainerBuilder)Activator.CreateInstance(type);
                return ((CardDataBuilder)builder.Builder())
                    .WithCardType("Leader")
                    .FreeModify(card =>
                    {
                        card.createScripts =
                        [
                            LeaderHelper.GiveUpgrade(),
                        ];
                    });
            }).ToList();
        _assets.AddRange(leaders);

        var menuLeaders = leaderTypes.Select(type =>
            {
                var builder = (ITrainerBuilder)Activator.CreateInstance(type);
                var menuModifiers = builder.MenuTrainerModifiers;
                var partners = menuModifiers.Partners;
                var cardBuilder = (CardDataBuilder)builder.Builder();
                cardBuilder
                    .ReplacePreRun([cardBuilder._data.name, .. partners])
                    .FreeModify(card =>
                    {
                        card.name = string.Join("And", [card.name, .. partners]);
                        card.mainSprite = Mod.GetSprite(menuModifiers.MenuSpriteName);
                        card.createScripts =
                        [
                            LeaderHelper.GiveUpgrade(),
                        ];
                    })
                    .WithCardType("Leader")
                    .WithTitle(menuModifiers.MenuTitle);
                LeaderNames.Add(cardBuilder._data.name);
                
                return cardBuilder;
            }).ToList();
        _assets.AddRange(menuLeaders);
        var enemies = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Cards.Enemies",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).Cast<CardDataBuilder>().ToList();
        _assets.AddRange(enemies);

        var charms = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Upgrades",
                    StringComparison.Ordinal)
                && typeof(IUpgradeBuilder).IsAssignableFrom(t))
            .Select(type => ((IUpgradeBuilder)Activator.CreateInstance(type)).Builder()).ToList();
        _assets.AddRange(charms);
        CharmNames = charms.Select(builder => builder._data.name).ToList();

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, builderNamespace + ".Tribes",
                    StringComparison.Ordinal)
                && typeof(IClassBuilder).IsAssignableFrom(t))
            .Select(type => ((IClassBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        Localization.CreateLocalizedStrings();
            
        _loaded = true;
    }

    private void RegisterTypes()
    {
        foreach (var type in Types.TypeChart)
        {
            var description = "";
            if (type.WeakTypes.Length > 0)
            {
                description += $"\nDeals <+2> damage to:\n{type.WeakTypes.Select(typeId => Types.TypeChart.First(t => typeId == t.Name).ReadableName()).Join()}";
            }
            if (type.ResistingTypes.Length > 0)
            {
                description += $"\nDeals <-2> damage to:\n{type.ResistingTypes.Select(typeId => Types.TypeChart.First(t => typeId == t.Name).ReadableName()).Join()}";
            }
            if (type.ImmuneTypes.Length > 0)
            {
                description += $"\nDeals <-4> damage to:\n{type.ImmuneTypes.Select(typeId => Types.TypeChart.First(t => typeId == t.Name).ReadableName()).Join()}";
            }
            _assets.Add(new KeywordDataBuilder(Instance)
                .Create(type.Keyword())
                .WithTitle(type.ReadableName())
                .WithTitleColour(KeywordColours.Orange)
                .WithDescription(description)
                .WithBodyColour(KeywordColours.White)
                .WithNoteColour(KeywordColours.Orange));

            _assets.Add(new StatusIconBuilder(Instance)
                .Create(type.Keyword(),
                    statusType: type.Keyword(),
                    Instance.ImagePath($"Icons/{type.Keyword()}.png").ToSprite())
                .WithIconGroupName(StatusIconBuilder.IconGroups.damage)
                .WithTextboxSprite()
                .WithKeywords(type.Keyword()));
                
            _assets.Add(new StatusEffectDataBuilder(Instance)
                .Create<StatusEffectType>(type.Name)
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .Subscribe_WithStatusIcon(type.Keyword())
                .SubscribeToAfterAllBuildEvent<StatusEffectType>(status =>
                {
                    status.type = type.Keyword();
                    status.weakTypes = type.WeakTypes;
                    status.resistingTypes = type.ResistingTypes;
                    status.immuneTypes = type.ImmuneTypes;
                    status.kasibBerry = Mod.GetStatus(ReduceSuperEffectiveDamage.Name);
                }));
        }
    }

    private void UnloadFromClasses()
    {
        // Remove data from Tribes
        var tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
        foreach (var pool in from tribe in tribes where tribe != null && tribe.rewardPools != null from pool in tribe.rewardPools where pool != null select pool)
        {
            pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == this);
        }
    }

    public override List<T> AddAssets<T, TY>()
    {
        if (_assets.OfType<T>().Any())
        {
            Debug.LogWarning($"[{Title}] adding {typeof(TY).Name}s: {_assets.OfType<T>().Select(a => a._data.name).Join()}");
        }
        return _assets.OfType<T>().ToList();
    }

    public static T[] RemoveNulls<T>(T[] data) where T : DataFile
    {
        var list = data.ToList();
        list.RemoveAll(x => x == null || x.ModAdded == Instance);
        return list.ToArray();
    }

    private static List<string> GetNamesFromBuilders(List<CardDataBuilder> data)
    {
        return data.Select(builder => builder._data.name).ToList();
    }
}