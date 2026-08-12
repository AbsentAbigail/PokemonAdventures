using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Keywords;

[UsedImplicitly]
public class Burn : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Mod.Instance)
            .Create(Name)
            .WithTitle("Burn")
            .WithTitleColour(KeywordColours.Red)
            .WithDescription("""
                             Reduces <keyword=attack>
                             Every turn, if <sprite name=overload>'d, double <sprite name=overload>|Counts down when activated
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Red)
            .WithCanStack(true);
    }
}