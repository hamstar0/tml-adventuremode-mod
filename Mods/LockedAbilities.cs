using System;
using HamstarHelpers.Helpers.Debug;
using LockedAbilities;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLockedAbilities() {
			var config = new LockedAbilitiesConfig {
				BackBraceEnabled = false,
				WorldGenChestImplantBackBraceChance = 0f
			};

			if( AdventureModeConfig.Instance.WorldGenRemoveDarkHeartPieces ) {
				config.WorldGenChestImplantDarkHeartPieceChance = 0f;
			}

			LockedAbilitiesConfig.Instance.OverlayChanges( config );
		}
	}
}
