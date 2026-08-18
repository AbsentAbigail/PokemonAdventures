using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Cast : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Cast")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("""
                             Summon specified weather with Intensity
                             If it already exists, increase its <sprite name=health> by <5> minus its Intensity, then increase its Intensity
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange);
    }
}