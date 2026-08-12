using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class LeafGuard : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Leaf Guard")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("While Harsh Sunlight is on the board, self and allies are immune to debuffs")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange);
    }
}