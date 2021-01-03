using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static bool GetSnowSignBaseCoordinates( out int tileX, out int tileY ) {
			int checkColumn( int myTileX ) {
				for( int myTileY=40; myTileY<WorldHelpers.DirtLayerTopTileY; myTileY++ ) {
					Tile tile = Main.tile[myTileX, myTileY];
					if( tile?.active() == true && tile.type == TileID.SnowBlock ) {
						return myTileY;
					}
				}
				return -1;
			}

			//

			int mid = Main.maxTilesX / 2;
			int dir = Main.spawnTileX > mid
				? -1
				: 1;
			int start = Main.spawnTileX > mid
				? Main.maxTilesX - 10
				: 1000;

			int corrCount = 0;

			for( int i=0; i<mid; i++ ) {
				tileX = start + (i * dir);
				tileY = checkColumn( tileX );
				if( tileY == -1 ) {
					continue;
				}

				corrCount++;
				if( corrCount >= 8 ) {
					return true;
				}
			}

			tileX = tileY = -1;
			return false;
		}


		////////////////

		public static void PlaceSnowSign( GenerationProgress progress ) {
			int left, top;
			if( !AMWorldGen.GetSnowSignBaseCoordinates( out left, out top ) ) {
				LogHelpers.Alert( "Could not find snow biome." );
				return;
			}

			string text = "Need to dig in somewhere for safety? Snow and sand make easy digging!";
			(int, int)? signAt = AMWorldGen.PlaceSign( left, top, "Snow", text );

			progress.Set( 1f );
		}
	}
}
