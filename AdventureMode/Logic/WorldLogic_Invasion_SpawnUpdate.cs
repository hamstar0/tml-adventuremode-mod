using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void UpdateWorldSpawnForInvasionStateIf() {
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
