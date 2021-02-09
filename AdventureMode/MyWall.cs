using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	class AMWall : GlobalWall {
		/*public override void KillWall( int i, int j, int type, ref bool fail ) {
			if( !Main.gameMenu ) {
				fail = true;	//!nonGameplay
			}
		}*/
		
		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			return Main.gameMenu;
		}
		public override bool KillSound( int i, int j, int type ) {
			return Main.gameMenu;
		}
	}
}