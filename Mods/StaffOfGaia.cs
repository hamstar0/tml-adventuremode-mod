using System;
using HamstarHelpers.Helpers.Debug;
using StaffOfGaia;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadStaffOfGaia() {
			StaffOfGaiaConfig.Instance.OverlayChanges( new StaffOfGaiaConfig {
				PlayerStartStaves = 0
			} );
		}
	}
}
