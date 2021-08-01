using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static bool GetJungleSignBaseCoordinates( out int leftTileX, out int floorTileY ) {
			int dirtTop = WorldLocationLibraries.DirtLayerTopTileY;

			int checkColumn( int myTileX ) {
				for( int myTileY=40; myTileY < dirtTop; myTileY++ ) {
					Tile tile = Main.tile[myTileX, myTileY];
					if( tile?.active() == true && tile.type == TileID.JungleGrass ) {
						return myTileY;
					}
				}
				return -1;
			}

			bool isValidSignColumn( int tileX, int fTileY ) {
				int top = fTileY - 16;
				for( int y = fTileY - 3; y > top; y-- ) {
					Tile tile = Main.tile[ tileX, y ];
					// No overhanging 'material' or caves
					if( tile?.active() == true || tile.wall > 0 ) {
						return false;
					}
				}
				return true;
			}

			//

			int jungleCount = 0;
			floorTileY = 0;

			if( Main.spawnTileX < (Main.maxTilesX/2) ) {    // Dungeon on left, jungle on right
				for( leftTileX=Main.maxTilesX/2; leftTileX<Main.maxTilesX; leftTileX++ ) {
					floorTileY = checkColumn( leftTileX );
					if( floorTileY == -1 || !isValidSignColumn(leftTileX, floorTileY) ) {
						continue;
					}

					jungleCount++;
					if( jungleCount >= 8 ) {
						return true;
					}
				}
			} else {    // Dungeon on right, jungle on left
				for( leftTileX=Main.maxTilesX/2; leftTileX>0; leftTileX-- ) {
					floorTileY = checkColumn( leftTileX );
					if( floorTileY == -1 || !isValidSignColumn(leftTileX, floorTileY) ) {
						continue;
					}

					jungleCount++;
					if( jungleCount >= 8 ) {
						return true;
					}
				}
			}

			return false;
		}


		////////////////

		public static void PlaceJungleSign( GenerationProgress progress ) {
			int leftX, floorY;
			if( !AMWorldGen.GetJungleSignBaseCoordinates( out leftX, out floorY ) ) {
				LogLibraries.Alert( "Could not find jungle." );
				return;
			}

			string text = "Beware!"
				+"\nDangerous wilderness ahead. Do not enter without the necessary protective items!"
				+"\n\nP.S. Do not pet the tortoises.";
			(int, int)? signAt = AMWorldGen.PlaceSign( leftX, floorY, "Jungle", text );

			if( signAt.HasValue ) {
				var myworld = ModContent.GetInstance<AMWorld>();
				myworld.JungleSignTile = signAt.Value;
			}

			progress.Set( 1f );
		}
	}
}
