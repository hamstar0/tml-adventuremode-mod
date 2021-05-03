using System;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadBullwhip() {
			Bullwhip.BullwhipConfig.Instance.SetOverride( nameof(Bullwhip.BullwhipConfig.WhipLedgePullStrength), 0f );
		}
	}
}
