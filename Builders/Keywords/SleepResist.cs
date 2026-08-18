using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class SleepResist : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Sleep Resist")
            .WithTitleColour(KeywordColours.Blue)
            .WithDescription("Can only have a maximum of <1><sprite name=sleep>")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Blue)
            .WithCanStack(false);
    }
}