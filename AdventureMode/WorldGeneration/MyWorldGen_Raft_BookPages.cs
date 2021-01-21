using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static IList<IList<string>> GetIntroTextLines() {
			var texts = new List<IList<string>> {
				new List<string> {
					"Welcome to Adventure Mode! This is a game mode built around journeying and exploration.",
					"This is designed to create a new experience upon existing content, but with a few twists.",
					"There's a lot of important new features and changes from the base game to note:",
				},
				new List<string> {
					"- Building and digging disabled (some exceptions).",
					"- Use house kits to create NPC houses, beds, crafting stations, and fast travel points.",
					"- All crafting is now handled at workbenches.",
				},
				new List<string> {
					"- Use platforms, planks, ropes, and rails kits to get around.",
					"- Use Orbs (found in chests) to open new areas.",
					//"- Grappling only works on platforms.",
					"- Ammo is more expensive; use wisely.",
				},
				new List<string> {
					"- Read item descriptions for more info.",
					"- Talk to the Guide for further help.",
					"- Do not whip the slimes!",
				},
			};

			if( AMConfig.Instance.OverrideRecipeTileRequirements ) {
				texts[1].Add( "- Use house kits to create NPC houses, beds, storage, and fast travel points." );
				texts[1].RemoveAt( 3 );
			}
			if( !AMConfig.Instance.EnableAlchemyRecipes ) {
				texts[3].Insert( 0, "- Alchemy and non-equipment recipes disabled." );
			}

			return texts;
		}

		
		public static IList<IList<string>> GetBriefingTextLines() {
			var texts = new List<IList<string>> {
				new List<string> {
					"Your ultimate goal is to end the undeath plague (Necrotis) threatening our entire world.",
					"Due to certain circumstances, your mission must be kept secret. You also must get by",
					"only with the limited resources we've provided and those you can find on the island.",
					"Sorry we can't be of more help.",
				},
				new List<string> {
					"Accompanying you is a guide of the island; you should be able to get a foothold at least.",
					"Here's the list of priorities for your mission:",
				},
				new List<string> {
					"- Safety first. Even with your special conditioning, dying will not help the mission.",
					"Watch your step.",
					"- Search for clues - anything - that can reveal the source of the plague. It must be",
					"somewhere on this island!",
					"- Previous expeditions and settlements have come before you, and all appear to have met",
					"their end. Find out why.",
				},
				new List<string> {
					"- Work quickly. The undeath phenomena increases with each day, and faster when living",
					"things tread nearby.",
					"- You may encounter powerful and dangerous beings here. Be ready to fight. We don't yet",
					"know what we're up against.",
					"- Do not whip the slimes! Just don't.",
				}
			};

			return texts;
		}


		////////////////

		public static string[] GetBookPages( IList<IList<string>> rawPages ) {
			string[] pages = new string[ rawPages.Count ];

			int i = 0;
			foreach( IList<string> lines in rawPages ) {
				pages[i++] = string.Join( "\n", lines );
			}

			return pages;
		}
	}
}
