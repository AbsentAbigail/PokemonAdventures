using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class ShroomResist : IKeywordBuilder
{
    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Shroom Resist")
            .WithTitleColour(KeywordColours.Trample)
            .WithDescription("Can only have a maximum of <1><sprite name=shroom>")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Trample)
            .WithCanStack(false);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}