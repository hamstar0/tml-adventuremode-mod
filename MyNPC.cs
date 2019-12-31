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
		public static void FilterShop( Item[] shop, IList<ItemDefinition> whitelist, ref int nextSlot ) {
			for( int i=0; i<shop.Length; i++ ) {
				Item item = shop[i];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !whitelist.Any( itemDef => itemDef.Type == item.type ) ) {
					for( int j=i; j<shop.Length-1; j++ ) {
						shop[j] = shop[j+1];
					}
					shop[ shop.Length-1 ] = new Item();

					nextSlot--;
					i--;
				}
			}
		}



		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var npcDef = new NPCDefinition( type );

			if( AdventureModeConfig.Instance.ShopWhitelists.ContainsKey( npcDef ) ) {
				AdventureModeNPC.FilterShop( shop.item, AdventureModeConfig.Instance.ShopWhitelists[npcDef], ref nextSlot );
			}

			switch( type ) {
			case NPCID.Merchant:
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
				break;
			}
		}
	}
}
