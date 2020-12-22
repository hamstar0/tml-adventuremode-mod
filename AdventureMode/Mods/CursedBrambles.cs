using System;
using CursedBrambles;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadCursedBrambles() {
			var config = CursedBramblesConfig.Instance;

			config.SetOverride( nameof(config.BossesCreateBrambleTrail), false );
			config.SetOverride( nameof(config.PlayersCreateDefaultBrambleTrail), false );
		}
	}
}
