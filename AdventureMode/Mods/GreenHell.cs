using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadGreenHell() {
			var ghConfig = GreenHell.GreenHellConfig.Instance;

			ghConfig.SetOverride( nameof(ghConfig.VerdantBlessingSoldByDryad), true );
			ghConfig.SetOverride( nameof(ghConfig.VerdantBlessingRecipeEnabled), false );
		}
	}
}
