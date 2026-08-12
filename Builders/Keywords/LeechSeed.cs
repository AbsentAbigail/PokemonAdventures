using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class LeechSeed : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Leech Seed")
            .WithTitleColour(KeywordColours.Orange)
            .WithDescription("Every turn, applier takes <sprite name=health>")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange)
            .WithCanStack(true);
    }
}