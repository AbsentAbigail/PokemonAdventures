using System;
using System.Collections.Generic;
using System.Linq;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Variables;
using Object = UnityEngine.Object;

namespace PokemonMod.Patches;

public static class JournalPatches
{
	[HarmonyPatch(typeof(Journal), nameof(Journal.OnEnable))]
	private class JournalOnEnable
	{
		[UsedImplicitly]
		private static void Postfix(Journal __instance)
		{
			if (!Pokemon.Instance.HasLoaded)
			{
				return;
			}
			
			var tabsTransform = __instance.transform.Find("Positioner").Find("Tabs");

			if (tabsTransform.Find("Pokedex") != null)
			{
				return;
			}
			
			var originalTabTransform = tabsTransform.Find("TabCards");
			var pokedexTabTransform = Object.Instantiate(originalTabTransform, originalTabTransform.parent);
			pokedexTabTransform.name = "Pokedex";
			pokedexTabTransform.SetAsFirstSibling();
			
			var originalTab = pokedexTabTransform.GetComponent<JournalTab>();
			var pokedexTab = originalTabTransform.GetComponent<JournalTab>();
			var cardManager = __instance.leftPageGroup.Find("Cards").GetComponent<JournalCardManager>();
		
			if (cardManager.populator.categories.All(category => category.name != "Pokedex"))
			{
				var category = new JournalCardManagerPopulator.Category
				{
					name = "Pokedex",
					enemy = false,
					cardTypes =
					[
						"Friendly",
						"Enemy",
					],
					cardNames =
					[
					],
				};
				cardManager.populator.categories = cardManager.populator.categories.AddToArray(category);
				foreach (var cardData in Pokedex.PokedexDictionary.Values)
				{
					category.CheckAdd(cardData, false);
				}
			}

			var categoryNames = cardManager.categoryNames;
			originalTab.onSelect.AddListener(() =>
			{
				for (var j = 0; j < cardManager.categoryNames.Length; j++)
				{
					cardManager.categoryNames = ["Pokedex", "Pokedex", "Pokedex"];
				}
				cardManager.SelectTab(0);
			});
			pokedexTab.onSelect.AddListener(delegate
			{
				for (var k = 0; k < cardManager.categoryNames.Length; k++)
				{
					cardManager.categoryNames = categoryNames;
				}
				cardManager.SelectTab(0);
			});

			if (Mod.ActionsOnUnload.ContainsKey("ResetJournalManagerCategories"))
			{
				return;
			}
			
			Mod.ActionsOnUnload.Add("ResetJournalManagerCategories", delegate
			{
				cardManager.categoryNames = cardManager.categoryNames.RemoveFromArray("Pokedex");
			});
		}
	}
	
	[HarmonyPatch(typeof(JournalCardManager), nameof(JournalCardManager.LoadCardData), typeof(JournalCardManagerPopulator.Category))]
	private class LoadJournalCardData
	{
		[UsedImplicitly]
		private static List<KeyValuePair<string, CardData>> Postfix(List<KeyValuePair<string, CardData>> __result, JournalCardManagerPopulator.Category category)
		{
			return category.name == "Pokedex" 
				? __result.OrderBy(keyValuePair => Array.IndexOf(Pokedex.Names, keyValuePair.Value.name.Replace(Pokemon.Instance.GUID + ".", ""))).ToList()
				: __result;
		}
	}
}