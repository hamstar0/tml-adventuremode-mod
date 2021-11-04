using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadRuinedItems() {
			var config = RuinedItems.RuinedItemsConfig.Instance;

			config.SetOverride( nameof(config.RuinedItemsLockedFromUse), false );
			config.SetOverride( nameof(config.CraftRuinPercentChance), 0.9f );
			config.SetOverride( nameof(config.PurchasedItemRuinPercentChance), 0.9f );
			config.SetOverride( nameof(config.NPCLootItemRuinPercentChance), 0.9f );
			config.SetOverride( nameof(config.WorldGenChestItemRuinPercentChance), 1f );
			config.SetOverride( nameof(config.MagitechScrapPrice), Item.buyPrice(0, 3, 0, 0) );
			//config.SetOverride( nameof(config.MagitechScrapAttemptsRepairOnlyOncePerItem), false );
			
			if( ModLoader.GetMod("AdventureModeLore") != null ) {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Cyborg ) );
			} else {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Merchant ) );
			}
		}
	}
}
