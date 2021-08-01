using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPowerfulMagic() {
			var pmConfig = PowerfulMagic.PowerfulMagicConfig.Instance;
			pmConfig.SetOverride( nameof(pmConfig.FocusManaChargeMaxRatePerSecond), 4f );	// was 5
		}
	}
}
