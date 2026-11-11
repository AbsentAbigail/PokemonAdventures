using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class BadlyShroomed : IKeywordBuilder
{
    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Badly Shroomed")
            .WithTitleColour(KeywordColours.DarkPurple)
            .WithDescription("Gain equal amount of <keyword=shroom> when taking <keyword=shroom> damage")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.DarkPurple)
            .WithCanStack(true);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}