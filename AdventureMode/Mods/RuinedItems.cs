using System;
using RuinedItems;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadRuinedItems() {
			var config = RuinedItemsConfig.Instance;

			config.SetOverride( nameof(config.RuinedItemsLockedFromUse), false );
		}
	}
}
