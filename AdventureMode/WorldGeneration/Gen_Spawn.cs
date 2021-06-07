using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static void SetBeachSpawn( GenerationProgress progress ) {
			bool foundNewSpawn = AMWorldGen.FindBeachSpawn( progress, out int tileX, out int tileY );

			if( foundNewSpawn ) {
				AMWorldGen.SetSpawn( tileX, tileY - 2 );
			}
		}


		////

		private static void SetSpawn( int x, int y ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			myworld.OldSpawn = (Main.spawnTileX, Main.spawnTileY);
			myworld.NewSpawn = (x, y);

			Main.spawnTileX = x;
			Main.spawnTileY = y;

			LogLibraries.Alert( "Spawn relocated to " + x + ", " + y );
		}


		////////////////

		private static bool FindBeachSpawn( GenerationProgress progress, out int tileX, out int tileY ) {
			int skyBot = WorldLocationLibraries.SkyLayerBottomTileY;
			int surfBot = WorldLocationLibraries.SurfaceLayerBottomTileY;

			bool checkColumns( int myTileX, out int myTileY ) {
				for( myTileY = skyBot; myTileY < surfBot; myTileY++ ) {
					Tile tile = Framing.GetTileSafely( myTileX, myTileY );
					if( tile == null || !tile.active() ) {
						continue;
					}

					if( Framing.GetTileSafely(myTileX, myTileY - 1).liquid != 0 ) {
						break;
					}

					return true;
				}
				return false;
			}

			//

			int reach = 40;//340;
			tileY = 0;

			if( Main.dungeonX > (Main.maxTilesX / 2) ) {
				int max = (Main.maxTilesX - reach) - Main.dungeonX;

				for( tileX = Main.maxTilesX - reach; tileX > Main.dungeonX; tileX-- ) {
					progress.Value = (float)( tileX - Main.dungeonX ) / (float)max;

					if( checkColumns(tileX, out tileY) ) {
						return true;
					}
				}
			} else {
				int max = Main.dungeonX;

				for( tileX = reach; tileX < max; tileX++ ) {
					progress.Value = (float)( tileX - reach ) / (float)max;
					
					if( checkColumns(tileX, out tileY) ) {
						return true;
					}
				}
			}

			return false;
		}
	}
}
