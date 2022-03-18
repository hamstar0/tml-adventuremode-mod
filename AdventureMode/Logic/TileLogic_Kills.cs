using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Services.Hooks.ExtendedHooks;
using FindableManaCrystals.Tiles;


namespace AdventureMode.Logic {
	static partial class TileLogic {
		public static void InitializeTileKillBehaviors() {
			void killWall( int i, int j, int type, ref bool fail, bool nonGameplay ) {
				fail = !nonGameplay;
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( TileLogic.OnKillTile );
			var killWallHook = new ExtendedTileHooks.KillWallDelegate( killWall );

			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
			ExtendedTileHooks.AddSafeWallKillHook( killWallHook );
		}


		////////////////

		private static void OnKillTile(
					int i,
					int j,
					int type,
					ref bool fail,
					ref bool effectOnly,
					ref bool noItem,
					bool nonGameplay ) {
			if( Main.gameMenu && Main.netMode != NetmodeID.Server ) {
				return;
			}
			if( noItem ) {
				return;
			}

			if( nonGameplay ) {
				noItem = type != TileID.Pots && type != TileID.Heart && type != ModContent.TileType<ManaCrystalShardTile>();
				return;
			}

			if( fail || effectOnly ) {
				return;
			}
			if( Main.netMode != NetmodeID.Server && !LoadLibraries.IsCurrentPlayerInGame() ) {
				return;
			}
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}
			
			// Arachnophobes, rejoice!
			if( type == TileID.Pots ) {
				TileLogic.KillPotTile( i, j, ref noItem );
			}
		}
	}
}
