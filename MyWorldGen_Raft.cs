using AdventureMode.Items;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HouseFurnishingKit.Items;
using MountedMagicMirrors.Tiles;
using System;
using Terraria;
using Terraria.ID;
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
		private static int[][] RaftImageTiles = new int[][] {	//124
			new int[] { 0,      0,      -1,		-1,		-1,		272,    4,      0,      0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,		-1,		272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,		-1,		272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,      0,      0,      0,      0 },
			new int[] { 0,      0,      18,     0,      0,      272,    0,      0,		21,		0,      0 },
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
						switch( tiles[y][x] ) {
						case TileID.Containers:
							int chestIdx = WorldGen.PlaceChest( left + x, top + y, 21, false, 5 );
							if( chestIdx != -1 ) {
								Main.chest[chestIdx].item[0] = new Item();
								Main.chest[chestIdx].item[0].SetDefaults( ModContent.ItemType<FramingPlankItem>() );
								Main.chest[chestIdx].item[0].stack = 99;
								Main.chest[chestIdx].item[1] = new Item();
								Main.chest[chestIdx].item[1].SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
								Main.chest[chestIdx].item[2] = new Item();
								Main.chest[chestIdx].item[2].SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
								Main.chest[chestIdx].item[3] = new Item();
								Main.chest[chestIdx].item[3].SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
							}
							break;
						case TileID.Cog:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							Main.tile[ left + x, top + y].inActive( true );
							break;
						case TileID.PlanterBox:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x], false, false, -1, 6 );
							break;
						default:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							break;
						}
					} else {
						//WorldGen.Place3x3Wall( left+x, top+y, (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 0 );	//3,1
						TilePlacementHelpers.Place3x3Wall( left+x, top+y, (ushort)ModContent.TileType<MountedMagicMirrorTile>() );	//2,1
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
