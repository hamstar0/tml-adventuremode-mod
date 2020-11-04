using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using PrefabKits.Items;
using Bullwhip.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void FilterShop( Item[] shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			for( int i = 0; i < shop.Length; i++ ) {
				Item item = shop[i];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !whitelist.Any( itemDef => itemDef.Type == item.type ) ) {
					for( int j = i; j < shop.Length - 1; j++ ) {
						shop[j] = shop[j + 1];
					}
					shop[shop.Length - 1] = new Item();

					nextSlot--;
					i--;
				}
			}
		}



		////////////////

		public static void FilterShops( int npcType, Item[] shopItems, ref int nextSlot ) {
			var npcDef = new NPCDefinition( npcType );

			if( AdventureModeConfig.Instance.ShopWhitelists.ContainsKey( npcDef ) ) {
				NPCLogic.FilterShop( shopItems, AdventureModeConfig.Instance.ShopWhitelists[npcDef], ref nextSlot );
			}
		}


		public static void SetupMerchantShop( Chest shop, ref int nextSlot ) {
			NPCLogic.SetupMerchantShopPrices( shop, nextSlot );
			NPCLogic.SetupMerchantShopNewInventory( shop, nextSlot );
		}

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
			var whip = new Item();
			var binocs = new Item();

			frameKit.SetDefaults( ModContent.ItemType<HouseFramingKitItem>() );
			furnKit.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
			whip.SetDefaults( ModContent.ItemType<BullwhipItem>() );
			binocs.SetDefaults( ItemID.Binoculars );

			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = frameKit;
			}
			if( nextSlot < shop.item.Length ) {
				shop.item[nextSlot++] = furnKit;
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
