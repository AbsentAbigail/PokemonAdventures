using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.Icons;

[UsedImplicitly]
public class ShroomResist : IIconBuilder
{
    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Mod.Instance)
            .Create(Name,
                "shroomresist",
                Mod.GetIconSprite("shroomresist"))
            .WithIconGroupName(StatusIconBuilder.IconGroups.health)
            .WithTextboxSprite()
            .WithKeywords(Keywords.ShroomResist.Name);
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}