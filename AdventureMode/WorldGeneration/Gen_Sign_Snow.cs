using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static bool GetSnowSignBaseCoordinates( out int tileX, out int tileY ) {
			int findTileInColumn( int myTileX ) {
				for( int myTileY=40; myTileY<WorldLocationLibraries.DirtLayerTopTileY; myTileY++ ) {
					Tile tile = Main.tile[ myTileX, myTileY ];
					if( tile?.active() == true && tile.type == TileID.SnowBlock ) {
						return myTileY;
					}
				}
				return -1;
			}

			bool isValidSignColumn( int myTileX, int myTileY ) {
				int top = myTileY - 16;
				for( int y = myTileY - 3; y > top; y-- ) {
					Tile tile = Main.tile[ myTileX, y ];
					if( tile?.active() == true || tile.wall > 0 ) {
						return false;
					}
				}
				return true;
			}

			//

			int mid = Main.maxTilesX / 2;
			int dir = Main.spawnTileX > mid
				? -1
				: 1;
			int start = Main.spawnTileX > mid
				? Main.maxTilesX - 10
				: 1000;

			int tilePadding = 0;

			for( int i=0; i<mid; i++ ) {
				tileX = start + (i * dir);
				tileY = findTileInColumn( tileX );
				if( tileY == -1 || !isValidSignColumn(tileX, tileY) ) {
					continue;
				}

				tilePadding++;
				if( tilePadding >= 8 ) {
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
				LogLibraries.Alert( "Could not find snow biome." );
				return;
			}

			string text = "Need to dig in somewhere for safety? Snow and sand make easy digging!";
			(int, int)? signAt = AMWorldGen.PlaceSign( left, top, "Snow", text );

			progress.Set( 1f );
		}
	}
}
