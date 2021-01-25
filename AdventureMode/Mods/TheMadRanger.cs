using System;
using TheMadRanger;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTheMadRanger() {
			var config = TMRConfig.Instance;

			config.SetOverride( nameof( config.BanditLootBandolierDropPercentChance ), 0f );
			config.SetOverride( nameof( config.BanditLootSpeedloaderDropPercentChance ), 0.02f );
			config.SetOverride( nameof( config.BanditLootGunDropPercentChance ), 0f );
			config.SetOverride( nameof( config.RecipeAvailableForSpeedloader ), false );
		}
	}
}
