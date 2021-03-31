using System;
using Terraria;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Logic {
	static partial class NPCLogic {
		public static void SetupGoblinTinkererShop( Chest shop, ref int nextSlot ) {
			var ductTape = new Item();
			ductTape.SetDefaults( ModContent.ItemType<MagicDuctTapeItem>() );

			shop.item[ nextSlot++ ] = ductTape;
		}
	}
}
