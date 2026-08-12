using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Sleep : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Sleep")
            .WithTitleColour(KeywordColours.Blue)
            .WithDescription("Unable to trigger|Counts down every turn")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Blue)
            .WithCanStack(true);
    }
}