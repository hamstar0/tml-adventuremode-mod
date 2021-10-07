using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.World;
using ReadableBooks.Items.ReadableBook;
using AdventureMode.Items;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static IList<IList<string>> GetIntroTextLines() {
			var config = AMConfig.Instance;
			var texts = new List<IList<string>> {
				new List<string> {
					"Welcome to Adventure Mode! This is a game mode built around journeying and exploration.",
					"This is designed to create a new experience upon existing content, but with a few twists.",
					"There's a lot of important new features and changes from the base game to note:",
				},
				new List<string> {
					"- Building and digging disabled (some exceptions).",
					"- Use house kits to create NPC houses, beds, crafting stations, and fast travel points.",
					"- All crafting is now handled at workbenches (or demon altars, where applicable).",
				},
				new List<string> {
					"- Use platforms, planks, ropes, and rails kits to get around.",
					"- Use Orbs (found in chests) to open new areas.",
					//"- Grappling only works on platforms.",
					"- Ammo is more expensive; use wisely.",
				},
				new List<string> {
					"- Always read item descriptions. You might find useful new info.",
					"- Talk to the Guide for further help.",
					"- Do not whip the slimes!",
				},
			};

			if( config.OverrideRecipeTileRequirements ) {
				texts[1].Add( "- Use house kits to create NPC houses, beds, storage, and fast travel points." );
				texts[1].RemoveAt( 3 );
			}
			if( config.StrangePlantsAddedPerBossSummonItemRecipe > 0 ) {
				texts[2].Add( "- Boss summon items now require Strange Plants. Don't trade/sell them." );
			}
			if( !config.EnableAlchemyRecipes ) {
				texts[2].Insert( 0, "- Alchemy and non-equipment recipes disabled." );
			}

			return texts;
		}


		////////////////

		internal static void SetupInitialSpawnInventory( AMPlayer myplayer, IList<Item> items ) {
			Item makeItem( int itemType, int stack ) {
				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;
				return item;
			}

			//

			myplayer.IsAdventurer = true;

			//

			items.Add( makeItem(ItemID.WoodenHammer, 1) );

			if( !AMConfig.Instance.EnableTorchRecipes ) {
				items.Add( makeItem(ItemID.Torch, 15) );
			}

			int resurfPotType = ModContent.ItemType<ResurfacePotionItem>();

			switch( WorldLibraries.GetSize() ) {
			case WorldSize.SubSmall:
				items.Add( makeItem( ItemID.RopeCoil, 15 ) );
				items.Add( makeItem( resurfPotType, 3 ) );
				break;
			case WorldSize.Small:
				items.Add( makeItem( ItemID.RopeCoil, 20 ) );
				items.Add( makeItem( resurfPotType, 5 ) );
				break;
			case WorldSize.Medium:
				items.Add( makeItem( ItemID.RopeCoil, 30 ) );
				items.Add( makeItem( resurfPotType, 8 ) );
				break;
			case WorldSize.Large:
				items.Add( makeItem( ItemID.RopeCoil, 40 ) );
				items.Add( makeItem( resurfPotType, 12 ) );
				break;
			case WorldSize.SuperLarge:
				items.Add( makeItem( ItemID.RopeCoil, 45 ) );
				items.Add( makeItem( resurfPotType, 15 ) );
				break;
			}

			items.Add( makeItem(ItemID.ClimbingClaws, 1) );

			//items.Add( makeItem(ItemID.GrapplingHook, 1) );

			items.Add( PlayerLogic.CreateGuideBook() );

			items.Add( makeItem(ModContent.ItemType<ResurfacePotionItem>(), 3) );
		}


		////

		private static Item CreateGuideBook() {
			string[] pages = PlayerLogic.GetIntroTextLines()
				.Select( lines => string.Join("\n", lines) )
				.ToArray();

			return ReadableBookItem.CreateBook(
				title: "- Intro To Adventure Mode -",
				pages: pages
			);
		}
	}
}
