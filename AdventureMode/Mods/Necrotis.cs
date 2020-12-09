using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Tiles;
using Necrotis;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNecrotis() {
			var config = NecrotisConfig.Instance;

			config.SetOverride( nameof(config.CanopicJarRecipeEnabled), false );
		}
	}
}
