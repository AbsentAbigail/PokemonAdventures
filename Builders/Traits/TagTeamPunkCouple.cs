using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;

namespace PokemonMod.Builders.Traits;

[UsedImplicitly]
public class TagTeamPunkCouple : ITraitBuilder
{
    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Mod.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.keyword = Mod.GetKeyword(TagTeam.Name);
                trait.effects =
                [
                    Mod.GetStatus(StatusEffects.TagTeamPunkCouple.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}