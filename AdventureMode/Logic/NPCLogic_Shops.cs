using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader.Config;
using ModLibsGeneral.Libraries.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void FilterShop( Item[] shopItems, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			for( int i = 0; i < shopItems.Length; i++ ) {
				Item shopItem = shopItems[i];
				if( shopItem?.Is() != true ) {
					continue;
				}

				if( whitelist.Any(def => def.Type == shopItem.type) ) {
					continue;
				}

				for( int j = i; j < (shopItems.Length - 1); j++ ) {
					shopItems[ j ] = shopItems[ j+1 ];
				}
				shopItems[ shopItems.Length - 1 ] = new Item();

				nextSlot--;
				i--;
			}
		}



		////////////////

		public static void FilterShops( int npcType, Item[] shopItems, ref int nextSlot ) {
			var npcDef = new NPCDefinition( npcType );

			if( AMConfig.Instance.ShopWhitelists.ContainsKey( npcDef ) ) {
				NPCLogic.FilterShop( shopItems, AMConfig.Instance.ShopWhitelists[npcDef], ref nextSlot );
			}
		}
	}
}
