using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items;
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

			//

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

			//

			return texts;
		}


		////////////////

		internal static void SetupSpawnInventoryDataBeforeInWorld( IList<Item> items ) {
			Item MakeItem( int itemType, int stack ) {
				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;
				return item;
			}

			////
			
LogLibraries.Log($"DEBUG OUT 4a {items?.Count}");
			items.Add( MakeItem(ItemID.WoodenHammer, 1) );

			if( !AMConfig.Instance.EnableTorchRecipes ) {
				items.Add( MakeItem(ItemID.Torch, 15) );
			}

			items.Add( MakeItem(ItemID.ClimbingClaws, 1) );

			//items.Add( makeItem(ItemID.GrapplingHook, 1) );

			items.Add( PlayerLogic.CreateGuideBook() );
LogLibraries.Log($"DEBUG OUT 4b {items.Count}");
		}


		////

		internal static void SetupSpawnInventoryInWorld( Player player ) {
			int invIdx = 0;

			void AddInvItem( int itemType, int stack ) {
				Item invItemAt = player.inventory[invIdx];

				while( invItemAt?.Is() == true ) {	// stack check REQUIRED?!
					invIdx++;
					invItemAt = player.inventory[invIdx];

					if( invIdx >= player.inventory.Length ) {
						throw new ModLibsException( "Player inventory full." );
					}
				}

				//

				var item = new Item();
				item.SetDefaults( itemType );
				item.stack = stack;

				player.inventory[invIdx] = item;
			}

			////

			int resurfPotType = ModContent.ItemType<ResurfacePotionItem>();

			switch( WorldLibraries.GetSize() ) {
			case WorldSize.SubSmall:
				AddInvItem( ItemID.RopeCoil, 15 );
				AddInvItem( resurfPotType, 2 );
				break;
			case WorldSize.Small:
				AddInvItem( ItemID.RopeCoil, 20 );
				AddInvItem( resurfPotType, 3 );
				break;
			case WorldSize.Medium:
				AddInvItem( ItemID.RopeCoil, 30 );
				AddInvItem( resurfPotType, 6 );
				break;
			case WorldSize.Large:
				AddInvItem( ItemID.RopeCoil, 40 );
				AddInvItem( resurfPotType, 10 );
				break;
			case WorldSize.SuperLarge:
				AddInvItem( ItemID.RopeCoil, 45 );
				AddInvItem( resurfPotType, 12 );
				break;
			}
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
