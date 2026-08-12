using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;

namespace PokemonMod.Builders.Traits;

[UsedImplicitly]
public class TagTeamEeveeAndEevee : ITraitBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Mod.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.keyword = Mod.GetKeyword(Keywords.TagTeam.Name);
                trait.effects =
                [
                    Mod.GetStatus(TagTeamEevee.Name),
                ];
            });
    }
}