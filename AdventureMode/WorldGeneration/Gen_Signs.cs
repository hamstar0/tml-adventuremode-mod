using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static (int tileX, int tileY)? PlaceSign( int left, int top, string signType, string text ) {
			void clearTile( int x, int y ) {
				try {
					WorldGen.KillTile( x, y, false, false, true );
					Main.tile[ x, y ].ClearEverything();
				} catch { }
			}

			//

			clearTile( left-1, top );
			clearTile( left, top );
			clearTile( left+1, top );
			Main.tile[left - 1, top].active( true );
			Main.tile[left, top].active( true );
			Main.tile[left + 1, top].active( true );
			Main.tile[left - 1, top].type = TileID.Mudstone;
			Main.tile[left, top].type = TileID.Mudstone;
			Main.tile[left + 1, top].type = TileID.Mudstone;
			
			clearTile( left-1, top-1 );
			clearTile( left, top-1 );
			clearTile( left+1, top-1 );
			clearTile( left-1, top-2 );
			clearTile( left, top-2 );
			clearTile( left+1, top-2 );
			clearTile( left-1, top-3 );
			clearTile( left, top-3 );
			clearTile( left+1, top-3 );

			bool placed = false;
			int highest = top - 4;

			for( top += 1; top > highest; top-- ) {	// lazy
				if( WorldGen.PlaceSign( left, top, TileID.Signs ) ) {
					int signIdx = Sign.ReadSign( left, top, true );
					Main.sign[signIdx].text = text;

					LogLibraries.Log( signType+" sign placed at " + left + ", " + top );

					placed = true;
					break;
				} else {
					LogLibraries.Alert( "Could not place "+signType+" sign." );
				}
			}

			(int, int)? at = null;
			if( placed ) { at = (left, top); }
			return at;
		}
	}
}
