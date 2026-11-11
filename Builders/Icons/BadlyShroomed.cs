using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.Icons;

[UsedImplicitly]
public class BadlyShroomed : IIconBuilder
{
    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Mod.Instance)
            .Create(name: Name,
                statusType: "badlyshroomed",
                Mod.GetIconSprite("badlyshroomed"))
            .WithIconGroupName(StatusIconBuilder.IconGroups.health)
            .WithTextColour(KeywordColours.DarkLabel)
            .WithTextShadow(KeywordColours.DarkPurple)
            .WithTextboxSprite()
            .WithKeywords(Keywords.BadlyShroomed.Name);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}