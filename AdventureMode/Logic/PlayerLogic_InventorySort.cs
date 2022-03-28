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
			bool dumpItem( int idx ) {
				if( player.inventory[idx]?.Is() != true ) {
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
				player.inventory[dumpToIdx] = player.inventory[idx];
				player.inventory[idx] = new Item();

				return true;
			}

			//

			bool xferFirstOfTo( int toIdx, int itemType, int maxFroIdx=-1 ) {
				if( maxFroIdx == -1 ) {
					maxFroIdx = player.inventory.Length;
				}

				//

				if( !dumpItem(toIdx) ) {
					return false;
				}

				//

				int froIdx = 0;
				Item invItemAt = player.inventory[froIdx];

				//while( invItemAt?.active != true || invItemAt?.stack <= 0 || invItemAt?.type != itemType ) {
				while( invItemAt?.Is(itemType) != true ) {
					froIdx++;
					invItemAt = player.inventory[froIdx];

					if( froIdx >= maxFroIdx ) {
						return false;
					}
				}

				//
				
//Main.NewText( $"xferred {player.inventory[froIdx].HoverName} ({froIdx}) to {toIdx}" );
				player.inventory[toIdx] = player.inventory[froIdx];
				player.inventory[froIdx] = new Item();

				return true;
			}

			//

			int hotbarIdx = 0;

			if( xferFirstOfTo(hotbarIdx, ModContent.ItemType<Bullwhip.Items.BullwhipItem>()) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, ModContent.ItemType<TheMadRanger.Items.Weapons.TheMadRangerItem>()) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, ModContent.ItemType<Ergophobia.Items.FramingPlank.FramingPlankItem>()) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, ItemID.WoodPlatform) ) {
				hotbarIdx++;
			}
			if( xferFirstOfTo(hotbarIdx, ModContent.ItemType<SoulBarriers.Items.PBGItem>()) ) {
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
			int bookIdx = 40;
			while( xferFirstOfTo(bookIdx, bookType, 39) ) {
				bookIdx++;
			}
		}
	}
}
