using System;
using System.IO;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.U2D;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PokemonMod;

internal static class Mod
{
    public static readonly WildfrostMod Instance = Pokemon.Instance;
    public static readonly bool BetaArt = Pokemon.Instance.BetaArt;

    public static Sprite GetIconSprite(string spriteName)
    {
        var sprite = Pokemon.BetaSpriteAtlas?.GetSprite(spriteName);
        return sprite != null ? sprite : Instance.ImagePath($"Icons/{spriteName}.png").ToSprite();
    }
    
    public static Sprite GetSprite(string spriteName)
    {
        if (BetaArt)
        {
            return GetBetaSprite(spriteName) ?? GetReleaseSprite(spriteName) ?? "".ToSprite();
        }
        
        return GetReleaseSprite(spriteName) ?? GetBetaSprite(spriteName) ?? "".ToSprite();
    }

    public static Sprite GetBackgroundSprite(string spriteName)
    {
        return GetSprite(spriteName, Pokemon.BackgroundSpriteAtlas, "Backgrounds/");
    }

    private static Sprite GetSprite(string spriteName, SpriteAtlas spriteAtlas, string pathPrefix = "")
    {
        var sprite = spriteAtlas?.GetSprite(spriteName);
        if (sprite != null)
        {
            return sprite;
        }
        
        var path = Pokemon.Instance.ImagePath($"{pathPrefix}{spriteName}.png");
        if (!File.Exists(path))
        {
            return null;
        }
        var texture = path.ToTex();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 1f), 160, 0U, SpriteMeshType.FullRect);
    }
    
    public static string PrefixGuid(string name)
    {
        return Extensions.PrefixGUID(name, Pokemon.Instance);
    }

    public static StatusEffectData GetStatus(string statusName)
    {
        return TryGet<StatusEffectData>(statusName);
    }

    public static CardData.StatusEffectStacks SStack(string statusName, int amount = 1)
    {
        return new CardData.StatusEffectStacks(
            GetStatus(statusName),
            amount);
    }

    public static CardData.TraitStacks TStack(string traitName, int amount = 1)
    {
        return new CardData.TraitStacks(
            GetTrait(traitName),
            amount);
    }

    public static T GetStatusOf<T>(string statusName) where T : StatusEffectData
    {
        return TryGet<T>(statusName);
    }

    public static CardData GetCard(string cardName)
    {
        return TryGet<CardData>(cardName);
    }

    public static CardUpgradeData GetCardUpgrade(string cardUpgradeName)
    {
        return TryGet<CardUpgradeData>(cardUpgradeName);
    }

    public static TraitData GetTrait(string traitName)
    {
        return TryGet<TraitData>(traitName);
    }

    public static KeywordData GetKeyword(string keywordName)
    {
        return TryGet<KeywordData>(keywordName);
    }

    public static ClassData GetTribe(string tribeName)
    {
        return TryGet<ClassData>(tribeName);
    }

    public static CardType GetCardType(string cardTypeName)
    {
        return TryGet<CardType>(cardTypeName);
    }

    public static T TryGet<T>(string datafileName) where T : DataFile
    {
        T dataFile;
        if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
        {
            dataFile = Pokemon.Instance.Get<StatusEffectData>(datafileName) as T;
        }
        else
        {
            dataFile = Pokemon.Instance.Get<T>(datafileName);
        }

        return dataFile ??
               throw new Exception(
                   $"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{datafileName}] or [{Extensions.PrefixGUID(datafileName, Pokemon.Instance)}]");
    }

    public static StatusEffectDataBuilder StatusCopy(string oldName, string newName)
    {
        var data = GetStatus(oldName).InstantiateKeepName();
        data.name = PrefixGuid(newName);
        var builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
        builder.Mod = Pokemon.Instance;
        return builder;
    }

    public static ClassDataBuilder TribeCopy(string oldName, string newName)
    {
        var data = GetTribe(oldName).InstantiateKeepName();
        data.name = PrefixGuid(newName);
        var builder = data.Edit<ClassData, ClassDataBuilder>();
        builder.Mod = Pokemon.Instance;
        return builder;
    }

    public static string CardTag(string name)
    {
        return $"<card={PrefixGuid(name)}>";
    }

    public static string VanillaCardTag(string name)
    {
        return $"<card={name}>";
    }

    public static string KeywordTag(string name)
    {
        return $"<keyword={PrefixGuid(name)}>";
    }

    public static LocalizedString GetLocalizedString(string name, string collection = "UI Text")
    {
        return LocalizationHelper.GetCollection(collection, SystemLanguage.English).GetString(name);
    }
    
    private static Sprite GetReleaseSprite(string spriteName)
    {
        return GetSprite(spriteName, Pokemon.SpriteAtlas);
    }
    
    private static Sprite GetBetaSprite(string spriteName)
    {
        return GetSprite(spriteName, Pokemon.BetaSpriteAtlas, "BetaArt/");
    }
    
    // Code by Phan
    public static T CreateScriptableCardImage<T>(string name) where T : ScriptableCardImage
    {
        // Create a new GameObject that will host the ScriptableImage
        var ghostObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(T))
        {
            // HideAndDontSave so it doesn't get touched during gameplay, OR
            hideFlags = HideFlags.HideAndDontSave
        };

        // ensure the GameObject is kept in memory this session
        Object.DontDestroyOnLoad(ghostObject);

        // Set the GameObject's size to the card size
        ghostObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.8f, 5.7f);

        // The image will try to autofill to fit the RectTransform size
        ghostObject.GetComponent<Image>().preserveAspect = true;
        // This fixes the card being hoverable
        ghostObject.GetComponent<Image>().raycastTarget = false;

        return ghostObject.GetComponent<T>();
    }
}
