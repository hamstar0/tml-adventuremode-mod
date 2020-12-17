using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader.Config;


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

			if( AMConfig.Instance.ShopWhitelists.ContainsKey( npcDef ) ) {
				NPCLogic.FilterShop( shopItems, AMConfig.Instance.ShopWhitelists[npcDef], ref nextSlot );
			}
		}
	}
}
