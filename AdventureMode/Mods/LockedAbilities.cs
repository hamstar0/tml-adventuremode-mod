using System;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLockedAbilities() {
			var config = AMConfig.Instance;
			var laConfig = LockedAbilities.LockedAbilitiesConfig.Instance;

			if( config.WorldGenRemoveDarkHeartPieces ) {
				laConfig.SetOverride( nameof(laConfig.WorldGenChestImplantDarkHeartPieceChance), 0f );
			}

			laConfig.SetOverride( nameof(laConfig.BackBraceEnabled), false );
			laConfig.SetOverride( nameof(laConfig.BootLacesEnabled), false );
			laConfig.SetOverride( nameof(laConfig.FlyingCertificateEnabled), false );
			laConfig.SetOverride( nameof(laConfig.GrappleHarnessEnabled), false );
			laConfig.SetOverride( nameof(laConfig.GunPermitEnabled), false );
			laConfig.SetOverride( nameof(laConfig.MountReinEnabled), false );
			laConfig.SetOverride( nameof(laConfig.SafetyHarnessEnabled), false );
			//laConfig.SetOverride( nameof(laConfig.UtilitarianBeltEnabled), false );
			laConfig.SetOverride( nameof(laConfig.WorldGenChestImplantBackBraceChance), 0f );
			laConfig.SetOverride( nameof(laConfig.DoubleJumpsRequireGels), false );
		}
	}
}
