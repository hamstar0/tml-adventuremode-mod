using System;
using HamstarHelpers.Helpers.Debug;
using LockedAbilities;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadLockedAbilities() {
			var config = AdventureModeConfig.Instance;
			var laConfig = LockedAbilitiesConfig.Instance;

			if( config.WorldGenRemoveDarkHeartPieces ) {
				laConfig.SetOverride( nameof(LockedAbilitiesConfig.WorldGenChestImplantDarkHeartPieceChance), 0f );
			}

			laConfig.SetOverride( nameof(LockedAbilitiesConfig.BackBraceEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.BootLacesEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.FlyingCertificateEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.GrappleHarnessEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.GunPermitEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.MountReinEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.SafetyHarnessEnabled), false );
			//laConfig.SetOverride( nameof(LockedAbilitiesConfig.UtilitarianBeltEnabled), false );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.WorldGenChestImplantBackBraceChance), 0f );
			laConfig.SetOverride( nameof(LockedAbilitiesConfig.DoubleJumpsRequireGels), false );
		}
	}
}
