using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Configs;
using LockedAbilities;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLockedAbilities() {
			if( AdventureModeConfig.Instance.WorldGenRemoveDarkHeartPieces ) {
				var config = new LockedAbilitiesConfig {
					WorldGenChestImplantDarkHeartPieceChance = 0f
				};

				ModConfigStack.SetStackedConfigChangesOnly( config );
			}

			LockedAbilitiesConfig.Instance.OverlayChanges( new LockedAbilitiesConfig {
				BackBraceEnabled = false,
				WorldGenChestImplantBackBraceChance = 0f
			} );
		}
	}
}
