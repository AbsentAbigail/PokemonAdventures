using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Paralysis : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Paralysis")
            .WithTitleColour(KeywordColours.Orange)
            .WithDescription("50% chance not to count down <sprite name=counter> each turn|Counts down every turn")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange)
            .WithCanStack(true);
    }
}