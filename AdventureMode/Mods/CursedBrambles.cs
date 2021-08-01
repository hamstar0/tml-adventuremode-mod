using System;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadCursedBrambles() {
			var config = CursedBrambles.CursedBramblesConfig.Instance;

			config.SetOverride( nameof(config.BossesCreateBrambleTrail), false );
			config.SetOverride( nameof(config.PlayersCreateDefaultBrambleTrail), false );
		}
	}
}
