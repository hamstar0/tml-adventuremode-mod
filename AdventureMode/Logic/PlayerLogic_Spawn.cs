using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Players;
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
		
		public static bool RetrofitPlayerInventory_If( Player player ) {
			if( !PlayerLibraries.IsPlayerVanillaFresh(player) ) {
				return false;
			}

			//

			IList<Item> invList = player.inventory.ToList();

			PlayerLogic.SetupInitialSpawnInventoryTemplate( invList );

			int i = 0;
			foreach( Item item in invList ) {
				player.inventory[i++] = item;
			}

			//

			PlayerLogic.ApplyRecommendedInventorySortion( player );

			return true;
		}


		////////////////

		internal static void SetupInitialSpawnInventoryTemplate( IList<Item> items ) {
			Item MakeItem( int itemType, int stack ) {
				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;
				return item;
			}

			//
			
			items.Add( MakeItem(ItemID.WoodenHammer, 1) );

			if( !AMConfig.Instance.EnableTorchRecipes ) {
				items.Add( MakeItem(ItemID.Torch, 15) );
			}

			items.Add( MakeItem(ItemID.ClimbingClaws, 1) );

			//items.Add( makeItem(ItemID.GrapplingHook, 1) );

			items.Add( PlayerLogic.CreateGuideBook() );
		}


		////

		internal static void SetupInWorldSpawnInventory( Player player ) {
			int invIdx = 0;

			void addInvItem( int itemType, int stack ) {
				if( invIdx >= player.inventory.Length ) {
					LogLibraries.Alert( "Player inventory full." );
					return;
				}

				while( player.inventory[invIdx]?.active == true ) {
					invIdx++;
					
					if( invIdx >= player.inventory.Length ) {
						LogLibraries.Alert( "Player inventory full." );
						return;
					}
				}

				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;

				player.inventory[invIdx] = item;
			}

			//

			int resurfPotType = ModContent.ItemType<ResurfacePotionItem>();

			switch( WorldLibraries.GetSize() ) {
			case WorldSize.SubSmall:
				addInvItem( ItemID.RopeCoil, 15 );
				addInvItem( resurfPotType, 2 );
				break;
			case WorldSize.Small:
				addInvItem( ItemID.RopeCoil, 20 );
				addInvItem( resurfPotType, 3 );
				break;
			case WorldSize.Medium:
				addInvItem( ItemID.RopeCoil, 30 );
				addInvItem( resurfPotType, 6 );
				break;
			case WorldSize.Large:
				addInvItem( ItemID.RopeCoil, 40 );
				addInvItem( resurfPotType, 10 );
				break;
			case WorldSize.SuperLarge:
				addInvItem( ItemID.RopeCoil, 45 );
				addInvItem( resurfPotType, 12 );
				break;
			}

			//

			PlayerLogic.ApplyRecommendedInventorySortion( player );
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
