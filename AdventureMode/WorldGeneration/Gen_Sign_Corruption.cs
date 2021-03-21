using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static bool GetCorruptionSignBaseCoordinates( out int tileX, out int tileY ) {
			int checkColumn( int myTileX ) {
				for( int myTileY=40; myTileY<WorldHelpers.DirtLayerTopTileY; myTileY++ ) {
					Tile tile = Main.tile[myTileX, myTileY];
					if( tile?.active() != true ) {
						continue;
					}
					switch( tile.type ) {
					case TileID.CorruptGrass:
					case TileID.FleshGrass:
					case TileID.FleshIce:
					case TileID.CorruptIce:
					case TileID.Ebonsand:
					case TileID.Crimsand:
						return myTileY;
					}
				}
				return -1;
			}

			bool isValidSignColumn( int myTileX, int myTileY ) {
				int top = myTileY - 16;
				for( int y = myTileY - 3; y > top; y-- ) {
					Tile tile = Main.tile[myTileX, y];
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
				? Main.maxTilesX
				: 0;

			int corrCount = 0;

			for( int i=40; i<Main.maxTilesX; i++ ) {
				tileX = start + (i * dir);
				tileY = checkColumn( tileX );
				if( tileY == -1 || !isValidSignColumn(tileX, tileY) ) {
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

		public static void PlaceCorruptionSign( GenerationProgress progress ) {
			int left, top;
			if( !AMWorldGen.GetCorruptionSignBaseCoordinates( out left, out top ) ) {
				LogHelpers.Alert( "Could not find corruption/crimson." );
				return;
			}

			string text = "Beware!\nBio-hazardous environment ahead. Recommend building elevated rail system for traversal.";
			(int, int)? signAt = AMWorldGen.PlaceSign( left, top, "Corruption", text );

			progress.Set( 1f );
		}
	}
}
