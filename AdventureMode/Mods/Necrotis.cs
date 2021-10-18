using System;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNecrotis() {
			var config = Necrotis.NecrotisConfig.Instance;

			config.SetOverride( nameof(config.CanopicJarRecipeEnabled), false );
			config.SetOverride( nameof(config.ElixirVanillaRecipeEnabled), false );
			config.SetOverride( nameof(config.ElixirAdventureRecipeEnabled), true );
		}
	}
}
