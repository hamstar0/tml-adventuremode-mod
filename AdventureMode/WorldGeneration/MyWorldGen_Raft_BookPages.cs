using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static IList<string> GetIntroTextLines() {
			var texts = new List<string> {
				// Page 1
				"Welcome to Adventure Mode! This is a game mode built around journeying and exploration.",
				"This is designed to create a new experience upon existing content, but with a few twists.",
				"There's a lot of important new features and changes from the base game to note:",
				// Page 2
				"- Building and digging disabled (some exceptions).",
				"- Use house kits to create NPC houses, beds, crafting stations, and fast travel points.",
				"- Crafting stations are either found or kit-made.",
				// Page 3
				"- Use platforms, planks, and ropes to get around.",
				"- Get Orbs from chests or challenges to progress.",
				//"- Grappling only works on platforms.",
				"- Read item descriptions for more info.",
				// Page 4
				"- Talk to the Guide for further help.",
				"- Do not whip the slimes!",
			};

			if( AMConfig.Instance.RemoveRecipeTileRequirements ) {
				texts[2] = "- Use house kits to create NPC houses, beds, storage, and fast travel points.";
				texts.RemoveAt( 3 );
			}
			if( !AMConfig.Instance.EnableAlchemyRecipes ) {
				texts.Insert( 2, "- Alchemy and non-equipment recipes disabled." );
			}

			return texts;
		}

		
		public static IList<string> GetBriefingTextLines() {
			var texts = new List<string> {
				// Page 1
				"Your ultimate goal is to end the undeath plague (Necrotis) threatening our entire world. Due to certain ",
				"circumstances, your mission must be kept secret. You also must get by only with the limited resources we've ",
				"provided and those you can find on the island. Sorry we can't be of more help.",
				// Page 2
				"Accompanying you is a guide of the island; you should be able to get a foothold at least. Here's the list of ",
				"priorities for your mission:",
				"",
				// Page 3
				"- Safety first. Even with your special conditioning, dying will not help the mission. Watch your step.",
				"- Search for clues - anything - that can reveal the source of the plague. It must be somewhere on this island!",
				"- Previous expeditions and settlements have come before you, and all appear to have met their end. Find out why.",
				// Page 4
				"- Work quickly. The undeath phenomena increases with each day, and faster when living things tread nearby.",
				"- You may encounter powerful and dangerous beings here. Be ready to fight. We don't yet know what we're up against.",
				"- Do not whip the slimes! Just don't.",
			};

			if( AMConfig.Instance.RemoveRecipeTileRequirements ) {
				texts[2] = "- Use house kits to create NPC houses, beds, storage, and fast travel points.";
				texts.RemoveAt( 3 );
			}
			if( !AMConfig.Instance.EnableAlchemyRecipes ) {
				texts.Insert( 2, "- Alchemy and non-equipment recipes disabled." );
			}

			return texts;
		}


		////////////////

		public static string[] GetBookPages( IList<string> rawPages ) {
			string[] pages = new string[rawPages.Count / 3];

			for( int i = 0; i < pages.Length; i++ ) {
				pages[i] = rawPages[i * 3];

				if( ((i * 3) + 1) < rawPages.Count ) {
					pages[i] += "\n" + rawPages[ (i * 3) + 1 ];
				}
				if( ((i * 3) + 2) < rawPages.Count ) {
					pages[i] += "\n" + rawPages[ (i * 3) + 2 ];
				}
			}

			return pages;
		}
	}
}
