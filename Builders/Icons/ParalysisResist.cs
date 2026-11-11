using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.Icons;

[UsedImplicitly]
public class ParalysisResist : IIconBuilder
{
    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Mod.Instance)
            .Create(name: Name,
                statusType: "paralysisresist",
                Mod.GetIconSprite("paralysisresist"))
            .WithIconGroupName(StatusIconBuilder.IconGroups.counter)
            .WithTextboxSprite()
            .WithKeywords(Keywords.ParalysisResist.Name);
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}