using Deadpan.Enums.Engine.Components.Modding;
using PokemonMod.Builders.Traits;
using PokemonMod.Builders.Tribes;
using PokemonMod.Variables;
using UnityEngine;

namespace PokemonMod.Helpers;

public static class Extensions
{
    extension(CardDataBuilder builder)
    {
        public CardDataBuilder DropsBling(int amount)
        {
            return builder.WithValue(amount * 36);
        }

        public CardDataBuilder EvolvesInto(string evolvesInto)
        {
            Evolutions.EvolutionPairs.Add((builder._data.name, evolvesInto));
            return builder;
        }

        // public CardDataBuilder TrainerLeader(string title, Sprite sprite, params string[] pokemon)
        // {
        //     
        // }
        
        public CardDataBuilder ReplacePreRun(params string[] replacements)
        {
            return builder.SubscribeToAfterAllBuildEvent(card =>
            {
                var replaceWith = new SaveCollection<string>(replacements);
                card.SetCustomData("pokemon.replacePreRun", replaceWith);
            });
        }

        public CardDataBuilder InUnitPool()
        {
            PokemonTribe.AddToUnitPool(builder._data.name);
            return builder;
        }
        
        public CardDataBuilder InItemPool()
        {
            PokemonTribe.AddToItemPool(builder._data.name);
            return builder;
        }
        
        public CardDataBuilder IsBerry()
        {
            PokemonTribe.AddToBerryPool(builder._data.name);
            return builder
                .SubscribeToAfterAllBuildEvent(card =>
                {
                    card.traits =
                    [
                        ..card.traits,
                        Mod.TStack("Consume"),
                        Mod.TStack(Berry.Name),
                    ];
                });
        }
    }

    public static object GetCustomDataOrNull(this CardData cardData, string key)
    {
        return cardData.customData?.Get<object>(key, null);
    }

    public static CardUpgradeDataBuilder InCharmPool(this CardUpgradeDataBuilder builder)
    {
        PokemonTribe.AddToCharmPool(builder._data.name);
        return builder;
    }
    
    public static StatusEffectDataBuilder IsReaction(this StatusEffectDataBuilder statusEffectDataBuilder)
    {
        return statusEffectDataBuilder
            .WithIsReaction(true)
            .FreeModify(status => status.descColorHex = "F99C61");
    }
}