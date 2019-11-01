using System;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeTile : GlobalTile {
		public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			return false;
		}

		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			fail = true;
			effectOnly = true;
			noItem = true;
		}

		public override bool Slope( int i, int j, int type ) {
			return false;
		}
		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			return false;
		}
		public override bool KillSound( int i, int j, int type ) {
			return false;
		}
	}
}