using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.Icons;

[UsedImplicitly]
public class Lum : IIconBuilder
{
    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Mod.Instance)
            .Create(name: Name,
                statusType: "lum",
                Mod.GetIconSprite("lum"))
            .WithIconGroupName(StatusIconBuilder.IconGroups.health)
            .WithTextColour(KeywordColours.DarkLabel)
            .WithTextShadow(KeywordColours.SleepDark)
            .WithTextboxSprite()
            .WithKeywords(Keywords.Lum.Name);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}