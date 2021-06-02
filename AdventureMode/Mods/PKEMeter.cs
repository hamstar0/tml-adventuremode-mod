using System;
using ModLibsCore.Libraries.Debug;
using PKEMeter;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPKEMeter() {
			PKEMeterConfig.Instance.SetOverride( nameof( PKEMeterConfig.PKEMeterHUDBasePositionX ), -224 );
		}
	}
}
