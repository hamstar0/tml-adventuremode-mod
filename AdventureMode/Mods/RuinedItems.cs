using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using RuinedItems;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadRuinedItems() {
			var config = RuinedItemsConfig.Instance;

			config.SetOverride( nameof(config.RuinedItemsLockedFromUse), false );

			if( ModLoader.GetMod("AdventureModeLore") != null ) {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Cyborg ) );
			} else {
				config.SetOverride( nameof( config.MagitechScrapSoldByWhom ), new NPCDefinition( NPCID.Merchant ) );
			}
		}
	}
}
