using System;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTheMadRanger() {
			var config = TheMadRanger.TMRConfig.Instance;

			config.SetOverride( nameof( config.BanditLootBandolierDropPercentChance ), 0f );
			config.SetOverride( nameof( config.BanditLootSpeedloaderDropPercentChance ), 0.005f );
			config.SetOverride( nameof( config.BanditLootGunDropPercentChance ), 0f );
			config.SetOverride( nameof( config.RecipeAvailableForSpeedloader ), false );
		}
	}
}
