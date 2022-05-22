using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupMerchantShop( Chest shop, ref int nextSlot ) {
			if( ModLoader.GetMod("Bullwhip") != null ) {
				NPCLogic.SetupMerchantShop_Bullwhip( shop, ref nextSlot );
			}

			var binocs = new Item();
			binocs.SetDefaults( ItemID.Binoculars );

			if( nextSlot >= shop.item.Length ) {
				LogLibraries.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[ nextSlot++ ] = binocs;

			if( !shop.item.Any(i=>i?.active == true && i.type == ItemID.Marshmallow) ) {
				var marshmallow = new Item();
				marshmallow.SetDefaults( ItemID.Marshmallow );

				shop.item[ nextSlot++ ] = marshmallow;
			}
		}

		////

		public static void SetupMerchantShop_Bullwhip( Chest shop, ref int nextSlot ) {
			var whip = new Item();
			whip.SetDefaults( ModContent.ItemType<Bullwhip.Items.BullwhipItem>() );

			if( nextSlot >= shop.item.Length ) {
				LogLibraries.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[nextSlot++] = whip;
		}
	}
}
