using System;
using Bullwhip;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadBullwhip() {
			BullwhipConfig.Instance.SetOverride( nameof(BullwhipConfig.WhipLedgePullStrength), 0f );
		}
	}
}
