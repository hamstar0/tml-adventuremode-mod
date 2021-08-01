using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static (int tileX, int tileY)? PlaceSign( int leftX, int floorY, string signType, string text ) {
			void clearTile( int x, int y ) {
				try {
					WorldGen.KillTile( x, y, false, false, true );
					Main.tile[ x, y ].ClearEverything();
				} catch { }
			}

			//

			clearTile( leftX-1, floorY );
			clearTile( leftX, floorY );
			clearTile( leftX+1, floorY );
			// Set mudstone base
			Main.tile[leftX - 1, floorY].active( true );
			Main.tile[leftX, floorY].active( true );
			Main.tile[leftX + 1, floorY].active( true );
			Main.tile[leftX - 1, floorY].type = TileID.Mudstone;
			Main.tile[leftX, floorY].type = TileID.Mudstone;
			Main.tile[leftX + 1, floorY].type = TileID.Mudstone;
			
			// Clear space above
			clearTile( leftX-1, floorY-1 );
			clearTile( leftX, floorY-1 );
			clearTile( leftX+1, floorY-1 );
			clearTile( leftX-1, floorY-2 );
			clearTile( leftX, floorY-2 );
			clearTile( leftX+1, floorY-2 );
			clearTile( leftX-1, floorY-3 );
			clearTile( leftX, floorY-3 );
			clearTile( leftX+1, floorY-3 );

			bool placed = false;
			int highest = floorY - 4;

			for( floorY += 1; floorY > highest; floorY-- ) {	// lazy
				if( WorldGen.PlaceSign( leftX, floorY, TileID.Signs ) ) {
					int signIdx = Sign.ReadSign( leftX, floorY, true );
					Main.sign[signIdx].text = text;

					LogLibraries.Log( signType+" sign placed at " + leftX + ", " + floorY );

					placed = true;
					break;
				} else {
					LogLibraries.Alert( "Could not place "+signType+" sign." );
				}
			}

			(int, int)? at = null;
			if( placed ) { at = (leftX, floorY); }
			return at;
		}
	}
}
