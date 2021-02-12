using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using RuinedItems;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadRuinedItems() {
			var config = RuinedItemsConfig.Instance;

			config.SetOverride( nameof(config.RuinedItemsLockedFromUse), false );
			config.SetOverride( nameof(config.PurchasedItemRuinPercentChance), 1f );
			config.SetOverride( nameof(config.NPCLootItemRuinPercentChance), 1f );
			config.SetOverride( nameof(config.WorldGenChestItemRuinPercentChance), 1f );
			config.SetOverride( nameof(config.MagitechScrapPrice), Item.buyPrice(0, 3, 0, 0) );

			if( ModLoader.GetMod("AdventureModeLore") != null ) {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Cyborg ) );
			} else {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Merchant ) );
			}
		}
	}
}
