using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPKEMeter() {
			var pkeConfig = PKEMeter.PKEMeterConfig.Instance;
			pkeConfig.SetOverride( nameof( pkeConfig.PKEMeterHUDBasePositionX ), -224 );
		}
	}
}
