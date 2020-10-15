using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using MountedMagicMirrors.Tiles;


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
			new int[] { 0,      0,      -1,		-1,     -1,     272,    4,      0,      0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,     -1,     272,    55,     55,     0,      0,      0 },
			new int[] { 0,      0,      -1,		-1,     -1,     272,    55,     55,     0,      0,      0 },
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

		public static void PlaceRaft( AdventureModeWorld myworld, GenerationProgress progress ) {
			int boatLeft, boatTop;
			AdventureModeWorldGen.GetBoatCoordinates( out boatLeft, out boatTop );

			AdventureModeWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
			AdventureModeWorldGen.PlaceWalls( myworld, progress, 0.25f, boatLeft, boatTop, AdventureModeWorldGen.RaftImageWalls );
			AdventureModeWorldGen.PlaceTiles( myworld, progress, 0.25f, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
			AdventureModeWorldGen.ProcessTiles( myworld, progress, 0.25f, boatLeft, boatTop, AdventureModeWorldGen.RaftImageTiles );
		}


		////

		public static void PlaceTiles(
					AdventureModeWorld myworld,
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
							AdventureModeWorldGen.PlaceStarterBarrel( myworld, left + x, top + y );
							break;
						case TileID.Cog:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x] );
							break;
						case TileID.PlanterBox:
							WorldGen.PlaceTile( left + x, top + y, tiles[y][x], false, false, -1, 6 );
							break;
						case TileID.Signs:
							if( WorldGen.PlaceSign( left + x, top + y, TileID.Signs ) ) {
								int signIdx = Sign.ReadSign( left + x, top + y, true );
								Main.sign[signIdx].text = AdventureModeWorldGen.GetIntroText();
							}
							break;
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
					AdventureModeWorld myworld, 
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

		public static void PlaceStarterBarrel( AdventureModeWorld myworld, int x, int y ) {
			int chestIdx = WorldGen.PlaceChest( x, y, TileID.Containers, false, 5 );
			if( chestIdx == -1 ) {
				return; // this occurs on the first pass
			}

			int i = 0;
			Item[] chestItems = Main.chest[chestIdx].item;

			foreach( ItemQuantityDefinition def in AdventureModeConfig.Instance.RaftBarrelContents ) {
				chestItems[i] = def.GetItem();
				i++;
			}

			myworld.RaftBarrelTile = (x, y);
		}

		public static void PlaceWalls(
					AdventureModeWorld myworld, 
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


		////////////////

		public static string GetIntroText() {
			var texts = new List<string> { "Welcome to Adventure Mode!",	//[c/00FF00:
				"- Building and digging disabled (some exceptions).",		//, e.g. treasures)."
				"- Use house kits to create NPC houses, beds, crafting stations, and fast travel points.",
				"- Crafting stations are either found or kit-made.",
				"- Use platforms, planks, and ropes to get around.",
				"- Get Orbs from chests or challenges to progress.",
				//"- Grappling only works on platforms.",
				"- Read item descriptions for more info.",
				"- Talk to the Guide for further help.",
				"- Do not whip the slimes!",
			};

			if( AdventureModeConfig.Instance.RemoveRecipeTileRequirements ) {
				texts[2] = "- Use house kits to create NPC houses, beds, storage, and fast travel points.";
				texts.RemoveAt( 3 );
			}
			if( !AdventureModeConfig.Instance.EnableAlchemyRecipes ) {
				texts.Insert( 2, "- Alchemy and non-equipment recipes disabled." );
			}

			return string.Join( "\n", texts );
		}
	}
}
