using System;
using HamstarHelpers.Helpers.Debug;
using PowerfulMagic;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPowerfulMagic() {
			var pmConfig = PowerfulMagicConfig.Instance;
			pmConfig.SetOverride( nameof(pmConfig.FocusManaChargeMaxRatePerSecond), 4f );	// was 5
		}
	}
}
