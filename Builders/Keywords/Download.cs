using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Download : IKeywordBuilder
{
    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Download")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("""
                             When deployed, copy all positive statuses and types of the front enemy
                             Then, increase own statuses
                             """)
            .WithCanStack(true)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Orange);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}