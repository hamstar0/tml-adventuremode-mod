using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using System;
using Terraria;
using Terraria.World.Generation;


namespace AdventureMode {
	partial class AdventureModeWorldGen {
		private static int[][] RaftImageWalls = new int[][] {
			new int[] {  },
			new int[] { 0,      0,      148,	148,	148 },
			new int[] { 0,      0,      0,		148,	148 },
			new int[] {  },
			new int[] {  },
			new int[] {  },
			new int[] {  },
			new int[] { 0,		42,     42,     42,     42,     42,     42,     42,     42,     42 },
		};
		private static int[][] RaftImageTiles = new int[][] {
			new int[] { 0,		0,		0,		0,		0,		124,	4,		0,		0,		0,		0 },
			new int[] { 0,		0,		0,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 0,		0,		0,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 0,		0,		0,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 0,		0,		0,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 0,		0,		0,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 0,		0,		18,		0,		0,		124,	0,		0,		0,		0,		0 },
			new int[] { 158,	158,	158,	158,	158,	158,	158,	158,	158,	158,	158 }
		};



		////////////////

		public static void GetBoatCoordinates( out int boatLeft, out int boatTop ) {
			boatLeft = Main.spawnTileX;
			boatTop = Main.spawnTileY;
			
			if( Main.spawnTileX < (Main.maxTilesX/2) ) {
				boatLeft -= 20;
			} else {
				boatLeft += 8;
			}

			for( Tile tile = Framing.GetTileSafely( boatLeft, boatTop );
				tile.liquid == 0;
				tile = Framing.GetTileSafely( boatLeft, ++boatTop ) ) {
			}

			boatTop -= AdventureModeWorldGen.RaftImageTiles.Length - 1;
		}

		public static void CreateBoat( GenerationProgress progress ) {
			int boatLeft, boatTop;
			AdventureModeWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );
			int[][] boatTiles = AdventureModeWorldGen.RaftImageTiles;
			int[][] boatWalls = AdventureModeWorldGen.RaftImageWalls;

			for( int y=0; y<boatTiles.Length; y++ ) {
				progress.Value = (float)y / (float)boatTiles.Length;

				for( int x=0; x<boatTiles[y].Length; x++ ) {
					if( boatTiles[y][x] == 0 ) { continue; }

					WorldGen.PlaceTile( boatLeft + x, boatTop + y, boatTiles[y][x] );
				}
			}

			for( int y=0; y< boatWalls.Length; y++ ) {
				progress.Value = (float)y / (float)boatWalls.Length;

				for( int x=0; x< boatWalls[y].Length; x++ ) {
					if( boatWalls[y][x] == 0 ) { continue; }

					WorldGen.PlaceWall( boatLeft + x, boatTop + y, boatWalls[y][x] );
					//Main.tile[boatLeft + x, boatTop + y].wall = (ushort)boatWalls[y][x];
				}
			}

			for( int y = 0; y < boatTiles.Length; y++ ) {
				progress.Value = (float)y / (float)boatTiles.Length;

				for( int x = 0; x < boatTiles[y].Length; x++ ) {
					if( boatTiles[y][x] == 0 ) { continue; }

					WorldGen.PlaceTile( boatLeft + x, boatTop + y, boatTiles[y][x] );
				}
			}
		}
	}
}
