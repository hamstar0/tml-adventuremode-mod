using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupMerchantShop( Chest shop, ref int nextSlot ) {
			if( ModLoader.GetMod("Bullwhip") != null ) {
				NPCLogic.SetupMerchantShop_Bullwhip( shop, ref nextSlot );
			}

			var binocs = new Item();
			binocs.SetDefaults( ItemID.Binoculars );

			if( nextSlot >= shop.item.Length ) {
				LogHelpers.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[ nextSlot++ ] = binocs;
		}

		////

		public static void SetupMerchantShop_Bullwhip( Chest shop, ref int nextSlot ) {
			var whip = new Item();
			whip.SetDefaults( ModContent.ItemType<Bullwhip.Items.BullwhipItem>() );

			if( nextSlot >= shop.item.Length ) {
				LogHelpers.Alert( "Merchant shop could not finish setup." );
				return;
			}
			shop.item[nextSlot++] = whip;
		}
	}
}
