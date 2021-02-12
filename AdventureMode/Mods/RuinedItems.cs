using System;
using Terraria.ID;
using Terraria.ModLoader.Config;
using RuinedItems;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadRuinedItems() {
			var config = RuinedItemsConfig.Instance;

			config.SetOverride( nameof(config.RuinedItemsLockedFromUse), false );
			config.SetOverride( nameof(config.MagitechScrapSoldByWhom), new NPCDefinition(NPCID.Cyborg) );
		}
	}
}
