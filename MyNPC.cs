using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using PrefabKits.Items;
using Bullwhip.Items;


namespace AdventureMode {
	partial class AdventureModeNPC : GlobalNPC {
		public static Item[] FilterShop( Item[] shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			Item[] newShop = new Item[ shop.Length ];

			Item item;
			int j = 0;
			for( int i=0; i<shop.Length; i++ ) {
				item = shop[i];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( whitelist.Any(itemDef => itemDef.Type == item.type) ) {
					newShop[j++] = item;
				} else {
					nextSlot--;
				}
			}

			for( ; j<shop.Length; j++ ) {
				newShop[j] = new Item();
			}

			return newShop;
		}



		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var npcDef = new NPCDefinition( type );

			if( AdventureModeConfig.Instance.ShopWhitelists.ContainsKey(npcDef) ) {
				Item[] newShop = AdventureModeNPC.FilterShop(
					shop.item,
					AdventureModeConfig.Instance.ShopWhitelists[npcDef],
					ref nextSlot
				);
				
				for( int i=0; i< shop.item.Length; i++ ) {
					shop.item[i] = newShop[i];
				}
			}

			switch( type ) {
			case NPCID.Merchant:
				var frameKit = new Item();
				var furnKit = new Item();
				var whip = new Item();

				frameKit.SetDefaults( ModContent.ItemType<HouseFramingKitItem>() );
				furnKit.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
				whip.SetDefaults( ModContent.ItemType<BullwhipItem>() );

				shop.item[nextSlot++] = frameKit;
				shop.item[nextSlot++] = furnKit;
				break;
			}
		}
	}
}
