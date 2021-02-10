using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.WorldGeneration {
	public class RaftComponents {
		public (int TileX, int TileY) Barrel { get; internal set; }

		public (int TileX, int TileY) Mirror { get; internal set; }


		////////////////

		public bool IsInitialized => this.Barrel != default && this.Mirror != default;
	}




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
				boatLeft -= 20;
			} else {
				boatLeft += 8;
			}

			for( Tile tile = Framing.GetTileSafely( boatLeft, boatTop );
				tile.liquid == 0;
				tile = Framing.GetTileSafely( boatLeft, ++boatTop ) ) {
			}

			boatTop -= AMWorldGen.RaftImageTiles.Length - 1;
		}

		////////////////

		public static void PlaceRaft( AMWorld myworld, GenerationProgress progress ) {
			int boatLeft, boatTop;
			AMWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );

			myworld.Raft = new RaftComponents();

			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, myworld.Raft );
			AMWorldGen.PlaceWalls( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageWalls, myworld.Raft );
			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, myworld.Raft );
			AMWorldGen.ProcessTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles, myworld.Raft );
		}
	}
}
