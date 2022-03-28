using System;
using Terraria;
using Terraria.ID;
using ModLibsGeneral.Libraries.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupTravellingMerchantShop( Chest shop, ref int nextSlot ) {
			int fishingPoleIdx = -1;

			for( int i = 0; i < nextSlot; i++ ) {
				Item item = shop.item[i];
				if( item?.Is() != true ) { continue; }

				if( item.type == ItemID.SittingDucksFishingRod ) {
					fishingPoleIdx = i;
					break;
				}
			}

			if( fishingPoleIdx != -1 ) {
				int i;
				for( i = fishingPoleIdx; i < nextSlot && i < (shop.item.Length - 1); i++ ) {
					shop.item[i] = shop.item[i + 1];
				}

				if( i == (shop.item.Length - 1) ) {
					shop.item[i] = new Item();
				}

				nextSlot--;
			}
		}
	}
}
