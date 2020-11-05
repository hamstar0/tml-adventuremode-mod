using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		private static void SetSpawn( int x, int y ) {
			LogHelpers.Alert( "Spawn relocated to " + x + ", " + y );
			Main.spawnTileX = x;
			Main.spawnTileY = y;
		}

		////

		public static void SetBeachSpawn( GenerationProgress progress ) {
			bool checkColumns( int x ) {
				for( int y = WorldHelpers.SkyLayerBottomTileY; y < WorldHelpers.SurfaceLayerBottomTileY; y++ ) {
					Tile tile = Framing.GetTileSafely( x, y );
					if( tile == null || !tile.active() ) {
						continue;
					}
					if( Main.tile[x, y - 1].liquid != 0 ) {
						break;
					}

					AMWorldGen.SetSpawn( x, y - 2 );
					return true;
				}
				return false;
			}

			int reach = 40;//340;

			if( Main.dungeonX > Main.maxTilesX / 2 ) {
				int max = (Main.maxTilesX - reach) - Main.dungeonX;

				for( int x = Main.maxTilesX - reach; x > Main.dungeonX; x-- ) {
					progress.Value = (float)(x - Main.dungeonX) / (float)max;

					if( checkColumns( x ) ) {
						break;
					}
				}
			} else {
				int max = Main.dungeonX;

				for( int x = reach; x < max; x++ ) {
					progress.Value = (float)( x - reach ) / (float)max;

					if( checkColumns( x ) ) {
						break;
					}
				}
			}
		}
	}
}
