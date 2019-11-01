using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		private static void SetSpawn( int x, int y ) {
			LogHelpers.Alert( "Spawn relocated to "+x+", "+y );
			Main.spawnTileX = x;
			Main.spawnTileY = y;
		}


		public static void SetBeachSpawn( GenerationProgress progress ) {
			bool checkColumns( int x ) {
				for( int y = WorldHelpers.SkyLayerBottomTileY; y < WorldHelpers.SurfaceLayerBottomTileY; y++ ) {
					Tile tile = Framing.GetTileSafely( x, y );
					if( tile == null || !tile.active() ) {
						continue;
					}
					if( Main.tile[x, y-1].liquid != 0 ) {
						break;
					}

					AdventureModeWorld.SetSpawn( x, y-2 );
					return true;
				}
				return false;
			}

			int reach = 40;//340;

			if( Main.dungeonX > Main.maxTilesX / 2 ) {
				int max = ( Main.maxTilesX - reach ) - Main.dungeonX;

				for( int x = Main.maxTilesX - reach; x > Main.dungeonX; x-- ) {
					progress.Value = (float)( x - Main.dungeonX ) / (float)max;

					if( checkColumns(x) ) {
						break;
					}
				}
			} else {
				int max = Main.dungeonX;

				for( int x = reach; x < max; x++ ) {
					progress.Value = (float)( x - reach ) / (float)max;

					if( checkColumns(x) ) {
						break;
					}
				}
			}
		}



		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				tasks.Insert( idx+1, new PassLegacy( "Adventure Mode: Default Spawn", ( progress ) => {
					AdventureModeWorld.SetBeachSpawn( progress );
					progress.Value = 1f;
				} ) );
			}
		}
	}
}
