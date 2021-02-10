using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		private static void SetSpawn( int x, int y ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			myworld.OldSpawn = (Main.spawnTileX, Main.spawnTileY);
			myworld.NewSpawn = (x, y);

			Main.spawnTileX = x;
			Main.spawnTileY = y;

			LogHelpers.Alert( "Spawn relocated to " + x + ", " + y );
		}


		////

		public static void SetBeachSpawn( GenerationProgress progress ) {
			bool checkColumns( int myTileX, out int myTileY ) {
				for( myTileY = WorldHelpers.SkyLayerBottomTileY; myTileY < WorldHelpers.SurfaceLayerBottomTileY; myTileY++ ) {
					Tile tile = Framing.GetTileSafely( myTileX, myTileY );
					if( tile == null || !tile.active() ) {
						continue;
					}
					if( Main.tile[myTileX, myTileY - 1].liquid != 0 ) {
						break;
					}

					return true;
				}
				return false;
			}

			//

			bool foundNewSpawn = false;
			int reach = 40;//340;
			int tileX, tileY=0;

			if( Main.dungeonX > (Main.maxTilesX / 2) ) {
				int max = (Main.maxTilesX - reach) - Main.dungeonX;

				for( tileX = Main.maxTilesX - reach; tileX > Main.dungeonX; tileX-- ) {
					progress.Value = (float)(tileX - Main.dungeonX) / (float)max;
					foundNewSpawn = checkColumns( tileX, out tileY );

					if( foundNewSpawn ) {
						break;
					}
				}
			} else {
				int max = Main.dungeonX;

				for( tileX = reach; tileX < max; tileX++ ) {
					progress.Value = (float)( tileX - reach ) / (float)max;
					foundNewSpawn = checkColumns( tileX, out tileY );

					if( foundNewSpawn ) {
						break;
					}
				}
			}

			if( foundNewSpawn ) {
				AMWorldGen.SetSpawn( tileX, tileY - 2 );
			}
		}
	}
}
