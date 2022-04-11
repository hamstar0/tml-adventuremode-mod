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
		
		public static bool RetrofitPlayerInventory_If( Player player ) {
			int tmrType = ModContent.ItemType<TheMadRanger.Items.Weapons.TheMadRangerItem>();
			int bandolierType = ModContent.ItemType<TheMadRanger.Items.Accessories.BandolierItem>();
			int whipType = ModContent.ItemType<Bullwhip.Items.BullwhipItem>();
			int pbgType = ModContent.ItemType<SoulBarriers.Items.PBGItem>();

			int tmrIdx = Array.FindIndex( player.inventory, i => i.type == tmrType );
			int bandolierIdx = Array.FindIndex( player.inventory, i => i.type == bandolierType );
			int whipIdx = Array.FindIndex( player.inventory, i => i.type == whipType );
			int pbgIdx = Array.FindIndex( player.inventory, i => i.type == pbgType );
			int binocIdx = Array.FindIndex( player.inventory, i => i.type == ItemID.Binoculars );

			var slotExceptions = new HashSet<int>();
			if( tmrIdx != -1 ) { slotExceptions.Add( tmrIdx ); }
			if( bandolierIdx != -1 ) { slotExceptions.Add( bandolierIdx ); }
			if( whipIdx != -1 ) { slotExceptions.Add( whipIdx ); }
			if( pbgIdx != -1 ) { slotExceptions.Add( pbgIdx ); }
			if( binocIdx != -1 ) { slotExceptions.Add( binocIdx ); }

			//

			bool isVanillaFresh = PlayerLibraries.IsPlayerVanillaFresh(
				player: player,
				inventorySlotExceptions: slotExceptions
			);

			if( !isVanillaFresh ) {
				return false;
			}

			//

			IList<Item> invList = player.inventory
				.Where( i => i?.active == true )
				.ToList();

			//

			if( tmrIdx == -1 ) {
				Item tmrItem = new Item();
				tmrItem.SetDefaults( tmrType, true );

				invList.Add( tmrItem );
			}
			
			if( bandolierIdx == -1 ) {
				Item bandolierItem = new Item();
				bandolierItem.SetDefaults( bandolierType, true );

				invList.Add( bandolierItem );
			}

			if( whipIdx == -1 ) {
				Item whipItem = new Item();
				whipItem.SetDefaults( whipType, true );

				invList.Add( whipItem );
			}

			if( pbgIdx == -1 ) {
				Item pbgItem = new Item();
				pbgItem.SetDefaults( pbgType, true );

				invList.Add( pbgItem );
			}

			if( binocIdx == -1 ) {
				Item binocItem = new Item();
				binocItem.SetDefaults( ItemID.Binoculars, true );

				invList.Add( binocItem );
			}

			//

			PlayerLogic.SetupSpawnInventoryDataBeforeInWorld( invList );

			//

			int i = 0;
			foreach( Item item in invList ) {
				player.inventory[i++] = item;
			}

			//

			PlayerLogic.SetupSpawnInventoryInWorld( player );

			//

			return true;
		}


		////////////////

		internal static void SetupSpawnInventoryDataBeforeInWorld( IList<Item> items ) {
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
