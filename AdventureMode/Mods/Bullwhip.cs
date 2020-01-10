using System;
using Bullwhip;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadBullwhip() {
			var config = new BullwhipConfig {
				WhipLedgePullStrength = 0f
			};

			BullwhipConfig.Instance.OverlayChanges( config );
		}
	}
}
