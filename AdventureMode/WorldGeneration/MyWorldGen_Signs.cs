using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static (int tileX, int tileY)? PlaceSign( int left, int top, string signType, string text ) {
			Main.tile[left - 1, top].ClearEverything();
			Main.tile[left, top].ClearEverything();
			Main.tile[left + 1, top].ClearEverything();
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

			bool placed = false;
			int highest = top - 4;

			for( top += 1; top > highest; top-- ) {	// lazy
				if( WorldGen.PlaceSign( left, top, TileID.Signs ) ) {
					int signIdx = Sign.ReadSign( left, top, true );
					Main.sign[signIdx].text = text;

					LogHelpers.Log( signType+" sign placed at " + left + ", " + top );

					placed = true;
					break;
				} else {
					LogHelpers.Alert( "Could not place "+signType+" sign." );
				}
			}

			(int, int)? at = null;
			if( placed ) { at = (left, top); }
			return at;
		}
	}
}
