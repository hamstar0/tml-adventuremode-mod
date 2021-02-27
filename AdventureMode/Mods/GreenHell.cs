using System;
using HamstarHelpers.Helpers.Debug;
using GreenHell;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadGreenHell() {
			var ghConfig = GreenHellConfig.Instance;

			ghConfig.SetOverride( nameof(ghConfig.VerdantBlessingSoldByDryad), true );
			ghConfig.SetOverride( nameof(ghConfig.VerdantBlessingRecipeEnabled), false );
		}
	}
}
