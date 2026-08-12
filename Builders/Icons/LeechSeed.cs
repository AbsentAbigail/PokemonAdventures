using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.Icons;

[UsedImplicitly]
public class LeechSeed : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Mod.Instance)
            .Create(name: Name,
                statusType: "leechseed",
                Mod.GetIconSprite("leechseed"))
            .WithIconGroupName(StatusIconBuilder.IconGroups.health)
            .WithTextColour(KeywordColours.White)
            .WithTextShadow(KeywordColours.Pink)
            .WithTextboxSprite()
            .WithKeywords(Keywords.LeechSeed.Name);
    }
}