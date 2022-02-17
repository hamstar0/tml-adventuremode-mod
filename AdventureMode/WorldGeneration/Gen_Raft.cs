﻿using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Data;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
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
		private static int[][] RaftImageTiles = new int[][] {	//124
			//new int[] { 0,      0,      -1,		-1,     -1,     272,    4,      0,      0,      0,      0 },
			//new int[] { 0,      0,      -1,		-1,     -1,     272,    55,     55,     0,      0,      0 },
			//new int[] { 0,      0,      -1,		-1,     -1,     272,    55,     55,     0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,     -1,     272,    4,      0,      0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,     -1,     272,    0,		0,		0,		0,		0 },
			new int[] { 0,      0,      -1,		-1,     -1,     272,    0,		0,		0,		0,		0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      18,     0,      0,      272,    0,      0,      21,     0,      0 },
			new int[] { 380,    380,    380,    380,    380,    380,    380,    380,    380,    380,    380 }	//158
		};



		////////////////

		public static void GetBoatCoordinates( out int boatLeft, out int boatTop ) {
			boatLeft = Main.spawnTileX;
			boatTop = Main.spawnTileY;

			if( Main.spawnTileX < (Main.maxTilesX / 2) ) {
				boatLeft -= 18;	//20;
			} else {
				boatLeft += 6;	//8;
			}

			for( Tile tile = Framing.GetTileSafely( boatLeft, boatTop );
				tile.liquid < 64;	// Shallow water
				tile = Framing.GetTileSafely( boatLeft, ++boatTop ) ) {
			}

			boatTop -= AMWorldGen.RaftImageTiles.Length - 1;
		}

		////////////////
		
		public static void PlaceRaft( AMWorld myworld, GenerationProgress progress ) {
			int boatLeft, boatTop;
			AMWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );

			var raft = new RaftWorldData();
			myworld.Raft = raft;

			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, ref raft );
			AMWorldGen.PlaceWalls( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageWalls, ref raft );
			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, ref raft );
			AMWorldGen.ProcessTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, ref raft );
		}
	}
}
