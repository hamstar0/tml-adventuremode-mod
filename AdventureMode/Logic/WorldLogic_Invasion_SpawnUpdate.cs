using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using Terraria.ModLoader;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void UpdateWorldSpawnForInvasionState() {
			var myworld = ModContent.GetInstance<AMWorld>();

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
