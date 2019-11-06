using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace AdventureMode {
	class AdventureModeWorldGen : ModWorld {
		private static int[][] BoatImageWalls = new int[][] {
			new int[] {  },
			new int[] { 0,      0,      148,	148,	148 },
			new int[] { 0,      0,      0,		148,	148 },
			new int[] {  },
			new int[] {  },
			new int[] {  },
			new int[] {  },
			new int[] { 0,		42,     42,     42,     42,     42,     42,     42,     42,     42 },
		};
		private static int[][] BoatImageTiles = new int[][] {
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

		private static void SetSpawn( int x, int y ) {
			LogHelpers.Alert( "Spawn relocated to " + x + ", " + y );
			Main.spawnTileX = x;
			Main.spawnTileY = y;
		}

		////

		public static void SetBeachSpawn( GenerationProgress progress ) {
			bool checkColumns( int x ) {
				for( int y = WorldHelpers.SkyLayerBottomTileY; y < WorldHelpers.SurfaceLayerBottomTileY; y++ ) {
					Tile tile = Framing.GetTileSafely( x, y );
					if( tile == null || !tile.active() ) {
						continue;
					}
					if( Main.tile[x, y - 1].liquid != 0 ) {
						break;
					}

					AdventureModeWorldGen.SetSpawn( x, y - 2 );
					return true;
				}
				return false;
			}

			int reach = 40;//340;

			if( Main.dungeonX > Main.maxTilesX / 2 ) {
				int max = (Main.maxTilesX - reach) - Main.dungeonX;

				for( int x = Main.maxTilesX - reach; x > Main.dungeonX; x-- ) {
					progress.Value = (float)(x - Main.dungeonX) / (float)max;

					if( checkColumns( x ) ) {
						break;
					}
				}
			} else {
				int max = Main.dungeonX;

				for( int x = reach; x < max; x++ ) {
					progress.Value = (float)( x - reach ) / (float)max;

					if( checkColumns( x ) ) {
						break;
					}
				}
			}
		}


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

			boatTop -= AdventureModeWorldGen.BoatImageTiles.Length - 1;
		}

		public static void CreateBoat( GenerationProgress progress ) {
			int boatLeft, boatTop;
			AdventureModeWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );
			int[][] boatTiles = AdventureModeWorldGen.BoatImageTiles;
			int[][] boatWalls = AdventureModeWorldGen.BoatImageWalls;

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




	class AdventureModeWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				tasks.Insert( idx + 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
					AdventureModeWorldGen.SetBeachSpawn( progress );
					progress.Value = 1f;
				} ) );
				tasks.Insert( idx + 2, new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
					AdventureModeWorldGen.CreateBoat( progress );
					progress.Value = 1f;
				} ) );
			}
		}
	}
}
