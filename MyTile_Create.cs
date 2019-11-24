using HamstarHelpers.Helpers.TModLoader;
using MountedMagicMirrors.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeTile : GlobalTile {
		public static bool IsStableForPlatform( int tileX, int tileY, int dirX ) {
			int max = AdventureModeConfig.Instance.MaxPlatformBridgeLength;
			if( max < 0 ) {
				return true;
			}

			for( int i=1; i <= max; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + (i * dirX), tileY );
				if( !tile.active() || !Main.tileSolid[tile.type] ) {
					break;
				}

				if( !Main.tileSolidTop[tile.type] && tile.type != TileID.MagicalIceBlock ) {
					return true;
				}
			}

			return false;
		}



		////////////////

		public override bool CanPlace( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			if( type == TileID.Platforms ) {
				return AdventureModeTile.IsStableForPlatform( i, j, -1 )
					|| AdventureModeTile.IsStableForPlatform( i, j, 1 );
			}
			if( type == ModContent.TileType<MountedMagicMirrorTile>() ) {
				return true;
			}
			if( AdventureModeConfig.Instance.TilePlaceWhitelist.Contains(TileID.GetUniqueKey(type)) ) {
				return true;
			}

			return false;
		}
	}
}