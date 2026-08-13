using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Constricted : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Constricted")
            .WithTitleColour(KeywordColours.Orange)
            .WithDescription("Deals damage every turn|Clears when applier is recalled or destroyed")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange)
            .WithCanStack(true);
    }
}