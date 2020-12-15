using System;
using Necrotis;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNecrotis() {
			var config = NecrotisConfig.Instance;

			config.SetOverride( nameof(config.CanopicJarRecipeEnabled), false );
		}
	}
}
