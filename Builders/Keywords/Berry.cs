using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Berry : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Berry")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("Can be fed to Pokemon")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange);
    }
}