﻿using HamstarHelpers.Helpers.Debug;
using MountedMagicMirrors.Tiles;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace AdventureMode {
	partial class AdventureModeWorldGen {
		private static int[][] RaftImageWalls = new int[][] {
			new int[] {  },
			new int[] { 0,      0,      148,    148,    148 },
			new int[] { 0,      0,      148,    148,    148 },
			new int[] { 0,      0,      148,    148,    148 },
			new int[] {  },
			new int[] {  },
			new int[] {  },
			new int[] { 0,      42,     42,     42,     42,     42,     42,     42,     42,     42 },
		};
		private static int[][] RaftImageTiles = new int[][] {
			new int[] { 0,      0,      0,      0,      0,      124,    4,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      -1,     0,      124,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      124,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      124,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      124,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      124,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      18,     0,      0,      124,    0,      0,      0,      0,      0 },
			new int[] { 158,    158,    158,    158,    158,    158,    158,    158,    158,    158,    158 }
		};



		////////////////

		public static void GetBoatCoordinates( out int boatLeft, out int boatTop ) {
			boatLeft = Main.spawnTileX;
			boatTop = Main.spawnTileY;

			if( Main.spawnTileX < ( Main.maxTilesX / 2 ) ) {
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

		////////////////

		public static void PlaceRaft( GenerationProgress progress ) {
			int boatLeft, boatTop;
			AdventureModeWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );

			AdventureModeWorldGen.PlaceTiles( progress, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
			AdventureModeWorldGen.PlaceWalls( progress, boatLeft, boatTop, AdventureModeWorldGen.RaftImageWalls );
			AdventureModeWorldGen.PlaceTiles( progress, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
		}


		////

		public static void PlaceTiles( GenerationProgress progress, int left, int top, int[][] tiles ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value = (float)y / (float)tiles.Length;

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					if( tiles[y][x] > 0 ) {
						WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
					} else {
						WorldGen.PlaceTile( left + x, top + y, ModContent.TileType<MountedMagicMirrorTile>() );
					}
				}
			}
		}

		public static void PlaceWalls( GenerationProgress progress, int left, int top, int[][] walls ) {
			for( int y = 0; y < walls.Length; y++ ) {
				progress.Value = (float)y / (float)walls.Length;

				for( int x = 0; x < walls[y].Length; x++ ) {
					if( walls[y][x] == 0 ) { continue; }

					WorldGen.PlaceWall( left + x, top + y, walls[y][x] );
					//Main.tile[boatLeft + x, boatTop + y].wall = (ushort)boatWalls[y][x];
				}
			}
		}
	}
}
