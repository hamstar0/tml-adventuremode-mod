using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ergophobia.Items;
using Bullwhip.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupMerchantShop( Chest shop, ref int nextSlot ) {
			NPCLogic.SetupMerchantShopPrices( shop, nextSlot );
			NPCLogic.SetupMerchantShopNewInventory( shop, nextSlot );
		}

		////

		private static void SetupMerchantShopPrices( Chest shop, int nextSlot ) {
			for( int i=0; i<nextSlot; i++ ) {
				Item item = shop.item[i];
				if( item?.active != true ) { continue; }

				switch( item.type ) {
				case ItemID.Rope:
				case ItemID.Torch:
				case ItemID.LesserHealingPotion:
					item.value *= 3;
					break;
				}
			}
		}

		private static void SetupMerchantShopNewInventory( Chest shop, int nextSlot ) {
			var frameKit = new Item();
			var furnKit = new Item();
			var scaffKit = new Item();
			var whip = new Item();
			var binocs = new Item();

			frameKit.SetDefaults( ModContent.ItemType<HouseFramingKitItem>() );
			furnKit.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
			scaffKit.SetDefaults( ModContent.ItemType<ScaffoldingErectorKitItem>() );
			whip.SetDefaults( ModContent.ItemType<BullwhipItem>() );
			binocs.SetDefaults( ItemID.Binoculars );

			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = frameKit;
			}
			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = furnKit;
			}
			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = scaffKit;
			}
			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = whip;
			}
			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = binocs;
			}
		}
	}
}
