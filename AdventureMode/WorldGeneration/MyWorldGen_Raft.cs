﻿using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using MountedMagicMirrors.Tiles;
using ReadableBooks.Items.ReadableBook;


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

			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles );
			AMWorldGen.PlaceWalls( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageWalls );
			AMWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles );
			AMWorldGen.ProcessTiles( myworld, progress, 0.25f, boatLeft, boatTop, AMWorldGen.RaftImageTiles );
		}


		////

		public static void PlaceTiles(
					AMWorld myworld,
					GenerationProgress progress,
					float progressStep,
					int left,
					int top,
					int[][] tiles ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value += progressStep / (float)tiles.Length;

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					if( tiles[y][x] > 0 ) {
						switch( tiles[y][x] ) {
						case TileID.Containers:
							AMWorldGen.PlaceStarterBarrel( myworld, left + x, top + y );
							break;
						case TileID.Cog:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							break;
						case TileID.PlanterBox:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x], false, false, -1, 6 );
							break;
						/*case TileID.Signs:
							if( WorldGen.PlaceSign( left + x, top + y, TileID.Signs ) ) {
								int signIdx = Sign.ReadSign( left + x, top + y, true );
								Main.sign[signIdx].text = AdventureModeWorldGen.GetIntroText();
							}
							break;*/
						default:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							break;
						}
					} else {
						//WorldGen.Place3x3Wall( left+x, top+y, (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 0 );	//3,1
						TilePlacementHelpers.Place3x3Wall( left + x, top + y, (ushort)ModContent.TileType<MountedMagicMirrorTile>() );  //2,1
					}
				}
			}
		}

		public static void ProcessTiles(
					AMWorld myworld, 
					GenerationProgress progress,
					float progressStep,
					int left,
					int top,
					int[][] tiles ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value += progressStep / (float)tiles.Length;

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					if( tiles[y][x] > 0 ) {
						switch( tiles[y][x] ) {
						case TileID.Cog:
							Main.tile[left + x, top + y].inActive( true );
							break;
						}
					}
				}
			}
		}

		public static void PlaceStarterBarrel( AMWorld myworld, int x, int y ) {
			int chestIdx = WorldGen.PlaceChest( x, y, TileID.Containers, false, 5 );
			if( chestIdx == -1 ) {
				return; // this occurs on the first pass (until raft floor exists)
			}

			int i = 0;
			Item[] chestItems = Main.chest[chestIdx].item;

			string[] pages = AMWorldGen.GetBriefingTextLines()
				.Select( lines => string.Join( "\n", lines ) )
				.ToArray();

			chestItems[i] = ReadableBookItem.CreateBook(
				title: "Your mission on Terraria",
				pages: pages
			);
			i++;

			foreach( ItemQuantityDefinition def in AMConfig.Instance.RaftBarrelContents ) {
				chestItems[i] = def.GetItem();
				i++;
			}

			myworld.RaftBarrelTile = (x, y);
		}

		public static void PlaceWalls(
					AMWorld myworld, 
					GenerationProgress progress,
					float progressStep,
					int left,
					int top,
					int[][] walls ) {
			for( int y = 0; y < walls.Length; y++ ) {
				progress.Value += progressStep / (float)walls.Length;

				for( int x = 0; x < walls[y].Length; x++ ) {
					if( walls[y][x] == 0 ) { continue; }

					WorldGen.PlaceWall( left + x, top + y, walls[y][x] );
					//Main.tile[boatLeft + x, boatTop + y].wall = (ushort)boatWalls[y][x];
				}
			}
		}
	}
}
