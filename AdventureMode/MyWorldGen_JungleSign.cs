using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureMode {
	partial class AdventureModeWorldGen {
		public static bool GetJungleSignBaseCoordinates( out int tileX, out int tileY ) {
			int checkColumn( int myTileX ) {
				for( int myTileY=40; myTileY<WorldHelpers.DirtLayerTopTileY; myTileY++ ) {
					Tile tile = Main.tile[myTileX, myTileY];
					if( tile?.active() == true && tile.type == TileID.JungleGrass ) {
						return myTileY;
					}
				}
				return -1;
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
					if( tileY == -1 ) {
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
			if( !AdventureModeWorldGen.GetJungleSignBaseCoordinates( out left, out top ) ) {
				LogHelpers.Alert( "Could not find jungle." );
				return;
			}

			Main.tile[left - 1, top].active( true );
			Main.tile[left, top].active( true );
			Main.tile[left + 1, top].active( true );
			Main.tile[left - 1, top].type = TileID.Mudstone;
			Main.tile[left, top].type = TileID.Mudstone;
			Main.tile[left + 1, top].type = TileID.Mudstone;
			
			Main.tile[left-1, top-1].ClearEverything();
			Main.tile[left-1, top-2].ClearEverything();
			Main.tile[left-1, top-3].ClearEverything();
			Main.tile[left, top-1].ClearEverything();
			Main.tile[left, top-2].ClearEverything();
			Main.tile[left, top-3].ClearEverything();
			Main.tile[left+1, top-1].ClearEverything();
			Main.tile[left+1, top-2].ClearEverything();
			Main.tile[left+1, top-3].ClearEverything();

			int highest = top - 4;
			for( top += 1; top > highest; top-- ) {	// lazy
				if( WorldGen.PlaceSign( left, top, TileID.Signs ) ) {
					int signIdx = Sign.ReadSign( left, top, true );
					Main.sign[signIdx].text = "Beware! Poisonous jungle and swamps ahead.";

					LogHelpers.Log( "Jungle sign placed at " + left + ", " + top );
					break;
				} else {
					LogHelpers.Alert( "Could not place jungle sign." );
				}
			}

			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			myworld.JungleSignLocation = (left, top);

			progress.Set( 1f );
		}
	}
}
