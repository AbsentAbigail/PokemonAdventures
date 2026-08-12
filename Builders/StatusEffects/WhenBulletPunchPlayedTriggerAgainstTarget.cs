using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBulletPunchPlayedTriggerAgainstTarget : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectTriggerWhenCardIsPlayed>(Name)
            .WithText($"Trigger against target hit with {Mod.CardTag(BulletPunch.Name)}")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .IsReaction()
            .SubscribeToAfterAllBuildEvent<StatusEffectTriggerWhenCardIsPlayed>(status =>
            {
                status.againstTarget = true;
                status.allowedCards =
                [
                    Mod.GetCard(BulletPunch.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}