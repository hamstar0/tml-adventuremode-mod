﻿using System;
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
			bool xferFirstOfTo( int toIdx, int itemType, int maxFroIdx=-1 ) {
				return PlayerLogic.XferFirstOfInventoryItemToPosition_If( player, toIdx, itemType, maxFroIdx );
			}

			//

			int hotbarIdx = 0;
			int bullwhipType = ModContent.ItemType<Bullwhip.Items.BullwhipItem>();
			int tmrType = ModContent.ItemType<TheMadRanger.Items.Weapons.TheMadRangerItem>();
			int plankType = ModContent.ItemType<Ergophobia.Items.FramingPlank.FramingPlankItem>();
			int pbgType = ModContent.ItemType<SoulBarriers.Items.PBGItem>();

			if( xferFirstOfTo(hotbarIdx, bullwhipType) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, tmrType) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, plankType) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, ItemID.WoodPlatform) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, pbgType) ) {
				hotbarIdx++;
			}

			xferFirstOfTo( 7, ItemID.WoodenHammer );
			xferFirstOfTo( 8, ItemID.RopeCoil );
			xferFirstOfTo( 9, ModContent.ItemType<ResurfacePotionItem>() );

			//

			xferFirstOfTo( 18, ModContent.ItemType<TheMadRanger.Items.Accessories.BandolierItem>() );
			xferFirstOfTo( 19, ItemID.ClimbingClaws );

			//
			
			int bookType = ModContent.ItemType<ReadableBooks.Items.ReadableBook.ReadableBookItem>();

			for( int bookIdx = 40; bookIdx < player.inventory.Length; bookIdx++ ) {
				if( !xferFirstOfTo(bookIdx, bookType, 39) ) {
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
