using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items;
using AdventureMode.Items;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static void ApplyRecommendedInventorySortion( Player player ) {
			bool XferFirstOfTo( int toIdx, int itemType, int maxFroIdx=-1 ) {
				return PlayerLogic.XferFirstOfInventoryItemToPosition_If( player, toIdx, itemType, maxFroIdx );
			}

			//

			int hotbarIdx = 0;
			int bullwhipType = ModContent.ItemType<Bullwhip.Items.BullwhipItem>();
			int tmrType = ModContent.ItemType<TheMadRanger.Items.Weapons.TheMadRangerItem>();
			int plankType = ModContent.ItemType<Ergophobia.Items.FramingPlank.FramingPlankItem>();
			int pbgType = ModContent.ItemType<SoulBarriers.Items.PBGItem>();

			if( XferFirstOfTo(hotbarIdx, bullwhipType) ) {
				hotbarIdx++;
			}
			if( XferFirstOfTo(hotbarIdx, tmrType) ) {
				hotbarIdx++;
			}
			if( XferFirstOfTo(hotbarIdx, plankType) ) {
				hotbarIdx++;
			}
			if( XferFirstOfTo(hotbarIdx, ItemID.WoodPlatform) ) {
				hotbarIdx++;
			}
			if( XferFirstOfTo(hotbarIdx, pbgType) ) {
				hotbarIdx++;
			}

			XferFirstOfTo( 7, ItemID.WoodenHammer );
			XferFirstOfTo( 8, ItemID.RopeCoil );
			XferFirstOfTo( 9, ModContent.ItemType<ResurfacePotionItem>() );

			//

			XferFirstOfTo( 18, ModContent.ItemType<TheMadRanger.Items.Accessories.BandolierItem>() );
			XferFirstOfTo( 19, ItemID.ClimbingClaws );

			//
			
			int bookType = ModContent.ItemType<ReadableBooks.Items.ReadableBook.ReadableBookItem>();

			for( int bookIdx = 40; bookIdx < player.inventory.Length; bookIdx++ ) {
				if( !XferFirstOfTo(bookIdx, bookType, 39) ) {
					break;
				}
			}
		}


		////////////////

		private static bool ShiftInventoryItemToBack( Player player, int index ) {
			if( player.inventory[index]?.Is() != true ) {
				return true;
			}

			//

			int dumpToIdx = 10;

			while( player.inventory[dumpToIdx]?.Is() == true ) {
				dumpToIdx++;

				if( dumpToIdx >= player.inventory.Length ) {
					return false;
				}
			}

			//

			//Main.NewText( $"dumped {player.inventory[idx].HoverName} ({idx}) to {dumpToIdx}" );
			player.inventory[dumpToIdx] = player.inventory[index];
			player.inventory[index] = new Item();

			return true;
		}


		private static bool XferFirstOfInventoryItemToPosition_If(
					Player player,
					int toIdx,
					int itemType,
					int maxFroIdx = -1 ) {
			if( maxFroIdx == -1 ) {
				maxFroIdx = player.inventory.Length;
			}

			//

			if( !PlayerLogic.ShiftInventoryItemToBack(player, toIdx) ) {
				return false;
			}

			//

			int froIdx = Array.FindIndex( player.inventory, i => i?.Is(itemType) == true );
			//while( invItemAt?.active != true || invItemAt?.stack <= 0 || invItemAt?.type != itemType ) {
			//while( invItemAt?.Is(itemType) != true ) {
			if( froIdx == -1 ) {
				return false;
			}

			//
			
			//Main.NewText( $"xferred {player.inventory[froIdx].HoverName} ({froIdx}) to {toIdx}" );
			player.inventory[toIdx] = player.inventory[froIdx];
			player.inventory[froIdx] = new Item();
			
			return true;
		}
	}
}
