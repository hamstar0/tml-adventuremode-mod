using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static bool GetJungleSignBaseCoordinates( out int tileX, out int tileY ) {
			int dirtTop = WorldLocationLibraries.DirtLayerTopTileY;

			int checkColumn( int myTileX ) {
				for( int myTileY=40; myTileY< dirtTop; myTileY++ ) {
					Tile tile = Main.tile[myTileX, myTileY];
					if( tile?.active() == true && tile.type == TileID.JungleGrass ) {
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

			int jungleCount = 0;
			tileY = 0;

			if( Main.spawnTileX < (Main.maxTilesX/2) ) {    // Dungeon on left, jungle on right
				for( tileX=Main.maxTilesX/2; tileX<Main.maxTilesX; tileX++ ) {
					tileY = checkColumn( tileX );
					if( tileY == -1 ) {
						continue;
					}

					jungleCount++;
					if( jungleCount >= 8 ) {
						return true;
					}
				}
			} else {    // Dungeon on right, jungle on left
				for( tileX=Main.maxTilesX/2; tileX>0; tileX-- ) {
					tileY = checkColumn( tileX );
					if( tileY == -1 || !isValidSignColumn(tileX, tileY) ) {
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
			int left, top;
			if( !AMWorldGen.GetJungleSignBaseCoordinates( out left, out top ) ) {
				LogLibraries.Alert( "Could not find jungle." );
				return;
			}

			string text = "Beware!"
				+"\nDangerous wilderness ahead. Do not enter without the necessary protective items!"
				+"\n\nP.S. Do not pet the tortoises.";
			(int, int)? signAt = AMWorldGen.PlaceSign( left, top, "Jungle", text );

			if( signAt.HasValue ) {
				var myworld = ModContent.GetInstance<AMWorld>();
				myworld.JungleSignTile = signAt.Value;
			}

			progress.Set( 1f );
		}
	}
}
