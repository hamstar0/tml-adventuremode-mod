using AdventureMode.Items;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HouseKits.Items;
using MountedMagicMirrors.Items;
using MountedMagicMirrors.Tiles;
using System;
using System.Collections.Generic;
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
			new int[] { 0,      0,      -1,		-1,		-1,		272,    55,		55,		0,		0,      0 },
			new int[] { 0,      0,      -1,		-1,		-1,		272,    55,		55,		0,		0,      0 },
			new int[] { 0,      0,      0,      0,      0,      272,    0,		0,		0,      0,      0 },
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
			AdventureModeWorldGen.ProcessTiles( progress, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
		}


		////

		public static void PlaceTiles( GenerationProgress progress, int left, int top, int[][] tiles ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value = (float)y / ((float)tiles.Length * 3f);

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					if( tiles[y][x] > 0 ) {
						switch( tiles[y][x] ) {
						case TileID.Containers:
							AdventureModeWorldGen.PlaceStarterBarrel( left + x, top + y );
							break;
						case TileID.Cog:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							break;
						case TileID.PlanterBox:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x], false, false, -1, 6 );
							break;
						case TileID.Signs:
							if( WorldGen.PlaceSign(left + x, top + y, TileID.Signs) ) {
								int signIdx = Sign.ReadSign( left + x, top + y, true );
								Main.sign[ signIdx ].text = "[c/00FF00:Welcome to Adventure Mode!]"
									+"\n- Building and digging disabled (some exceptions, e.g. treasure)."
									+"\n- Use house kits for NPC houses, beds, crafting, and fast travel."
									+"\n- Read item descriptions for more info."
									+"\n- Talk to the Guide for further tips."
									+"\n- Do not whip the slimes!";
							}
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
		public static void ProcessTiles( GenerationProgress progress, int left, int top, int[][] tiles ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value = (float)y / ((float)tiles.Length * 3f);

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					if( tiles[y][x] > 0 ) {
						switch( tiles[y][x] ) {
						case TileID.Cog:
							Main.tile[ left + x, top + y].inActive( true );
							break;
						}
					}
				}
			}
		}

		public static void PlaceStarterBarrel( int x, int y ) {
			int chestIdx = WorldGen.PlaceChest( x, y, 21, false, 5 );
			if( chestIdx == -1 ) {
				return;	// this occurs on the first pass
			}

			Item getBarrelItem( int type, int stack=1 ) {
				Item item;
				item = new Item();
				item.SetDefaults( type );
				item.stack = stack;
				return item;
			}
			IEnumerable<Item> getBarrelItems() {
				yield return getBarrelItem( ItemID.Wood, 99 );
				yield return getBarrelItem( ModContent.ItemType<FramingPlankItem>(), 99 );
				yield return getBarrelItem( ModContent.ItemType<HouseFurnishingKitItem>() );
				yield return getBarrelItem( ModContent.ItemType<HouseFurnishingKitItem>() );
				yield return getBarrelItem( ModContent.ItemType<HouseFurnishingKitItem>() );
				yield return getBarrelItem( ModContent.ItemType<HouseFramingKitItem>() );
				yield return getBarrelItem( ModContent.ItemType<HouseFramingKitItem>() );
				yield return getBarrelItem( ModContent.ItemType<MountableMagicMirrorTileItem>(), 2 );
			}

			int i = 0;
			foreach( Item item in getBarrelItems() ) {
				Main.chest[chestIdx].item[i++] = item;
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
