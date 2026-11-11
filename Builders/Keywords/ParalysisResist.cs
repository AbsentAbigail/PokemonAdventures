using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class ParalysisResist : IKeywordBuilder
{
    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Paralysis Resist")
            .WithTitleColour(KeywordColours.Paralysis)
            .WithDescription("Can only have a maximum of <1><sprite name=paralysis>")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Paralysis)
            .WithCanStack(false);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}