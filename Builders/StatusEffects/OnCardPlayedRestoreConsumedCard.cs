using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedRestoreConsumedCard : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectRecycleConsumeCards>(Name)
            .WithText("Choose a copy of a consumed card to add to hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectRecycleConsumeCards>(status =>
            {
                status.title = Mod.GetLocalizedString(Name);
                status.summonCopy = Mod.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}