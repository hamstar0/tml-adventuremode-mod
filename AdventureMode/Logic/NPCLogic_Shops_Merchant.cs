using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Bullwhip.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupMerchantShop( Chest shop, ref int nextSlot ) {
			var whip = new Item();
			var binocs = new Item();

			whip.SetDefaults( ModContent.ItemType<BullwhipItem>() );
			binocs.SetDefaults( ItemID.Binoculars );

			if( nextSlot >= shop.item.Length ) {
				LogHelpers.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[ nextSlot++ ] = whip;

			if( nextSlot >= shop.item.Length ) {
				LogHelpers.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[ nextSlot++ ] = binocs;
		}
	}
}
