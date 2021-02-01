using System;
using HamstarHelpers.Helpers.Debug;
using GreenHell;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadGreenHell() {
			GreenHellConfig.Instance.SetOverride( nameof(GreenHellConfig.VerdantBlessingSoldByDryad), true );
			GreenHellConfig.Instance.SetOverride( nameof(GreenHellConfig.VerdantBlessingRecipeEnabled), false );
		}
	}
}
