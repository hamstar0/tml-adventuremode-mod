using System;
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
		public static void PlaceTiles(
					AMWorld myworld,
					GenerationProgress progress,
					float progressStep,
					int left,
					int top,
					int[][] tiles,
					RaftComponents components ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value += progressStep / (float)tiles.Length;

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					int tileX = left + x;
					int tileY = top + y;

					if( tiles[y][x] > 0 ) {	// <- Can't know mod tile types in advance I guess
						switch( tiles[y][x] ) {	// <- Especially for switch use
						case TileID.Containers:
							if( AMWorldGen.PlaceStarterBarrel(myworld, tileX, tileY) ) {
								components.Barrel = (tileX, tileY);
							}
							break;
						case TileID.Cog:
							WorldGen.PlaceTile( tileX, tileY, tiles[y][x] );
							break;
						case TileID.PlanterBox:
							WorldGen.PlaceTile( tileX, tileY, tiles[y][x], false, false, -1, 6 );
							break;
						/*case TileID.Signs:
							if( WorldGen.PlaceSign( left + x, top + y, TileID.Signs ) ) {
								int signIdx = Sign.ReadSign( left + x, top + y, true );
								Main.sign[signIdx].text = AdventureModeWorldGen.GetIntroText();
							}
							break;*/
						default:
							WorldGen.PlaceTile( tileX, tileY, tiles[y][x] );
							break;
						}
					} else {
						var mirrorType = (ushort)ModContent.TileType<MountedMagicMirrorTile>();

						//WorldGen.Place3x3Wall( left+x, top+y, (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 0 );	//3,1
						TilePlacementHelpers.Place3x3Wall( tileX, tileY, mirrorType );  //2,1

						if( Main.tile[tileX, tileY].type == mirrorType ) {
							components.Mirror = (tileX, tileY);
						}
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
					int[][] tiles,
					RaftComponents components ) {
			for( int y = 0; y < tiles.Length; y++ ) {
				progress.Value += progressStep / (float)tiles.Length;

				for( int x = 0; x < tiles[y].Length; x++ ) {
					if( tiles[y][x] == 0 ) { continue; }

					int tileX = x + left;
					int tileY = y + top;

					if( tiles[y][x] > 0 ) {
						switch( tiles[y][x] ) {
						case TileID.Cog:
							Main.tile[tileX, tileY].inActive( true );
							break;
						}
					}
				}
			}
		}

		public static void PlaceWalls(
					AMWorld myworld,
					GenerationProgress progress,
					float progressStep,
					int left,
					int top,
					int[][] walls,
					RaftComponents components ) {
			for( int y = 0; y < walls.Length; y++ ) {
				progress.Value += progressStep / (float)walls.Length;

				for( int x = 0; x < walls[y].Length; x++ ) {
					if( walls[y][x] == 0 ) { continue; }

					int tileX = x + left;
					int tileY = y + top;

					WorldGen.PlaceWall( tileX, tileY, walls[y][x] );
					//Main.tile[boatLeft + x, boatTop + y].wall = (ushort)boatWalls[y][x];
				}
			}
		}


		////

		public static bool PlaceStarterBarrel( AMWorld myworld, int tileX, int tileY ) {
			int chestIdx = WorldGen.PlaceChest( tileX, tileY, TileID.Containers, false, 5 );
			if( chestIdx == -1 ) {
				return false; // this occurs on the first pass (until raft floor exists)
			}

			int i = 0;
			Item[] chestItems = Main.chest[chestIdx].item;

			//

			string[] pages = AMWorldGen.GetBriefingTextLines()
				.Select( lines => string.Join( "\n", lines ) )
				.ToArray();

			chestItems[i] = ReadableBookItem.CreateBook(
				title: "Your mission on Terraria",
				pages: pages
			);
			i++;

			//

			foreach( ItemQuantityDefinition def in AMConfig.Instance.RaftBarrelContents ) {
				chestItems[i] = def.GetItem();
				i++;
			}

			return true;
		}
	}
}
