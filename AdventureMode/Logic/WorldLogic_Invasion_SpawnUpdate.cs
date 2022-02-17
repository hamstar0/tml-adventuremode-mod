using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void UpdateWorldSpawnForInvasionState_If() {
			var myworld = ModContent.GetInstance<AMWorld>();
			if( !myworld.IsCurrentWorldAdventure ) {
				return;
			}

			if( Main.invasionType > 0 && Main.invasionSize > 0 ) {
				Main.spawnTileX = myworld.OldSpawn.TileX;
				Main.spawnTileY = myworld.OldSpawn.TileY;
			} else {
				Main.spawnTileX = myworld.NewSpawn.TileX;
				Main.spawnTileY = myworld.NewSpawn.TileY;
			}
		}
	}
}
