using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	class AdventureModeNPC : GlobalNPC {
		public static void FilterShop( IList<Item> shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			foreach( Item item in shop ) {
				if( whitelist.Any( itemDef => itemDef.Type == item.type ) ) {
					continue;
				}

				shop.Remove( item );
				nextSlot--;
			}
		}



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var npcDef = new NPCDefinition( type );

			if( AdventureModeMod.Config.ShopWhitelists.ContainsKey(npcDef) ) {
				var shopList = new List<Item>( shop.item );

				AdventureModeNPC.FilterShop( shopList, AdventureModeMod.Config.ShopWhitelists[npcDef], ref nextSlot );
				shop.item = shopList.ToArray();
			}
		}
	}
}
