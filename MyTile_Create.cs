using AdventureMode.Tiles;
using HamstarHelpers.Helpers.TModLoader;
using MountedMagicMirrors.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeTile : GlobalTile {
		public static bool IsSuitableForPlatform( int tileX, int tileY, int dirX ) {
			int max = AdventureModeConfig.Instance.MaxPlatformBridgeLength;
			if( max < 0 ) {
				return true;
			}

			for( int i=1; i <= max; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + (i * dirX), tileY );
				if( !tile.active() || !Main.tileSolid[tile.type] ) {
					break;
				}

				// Anchor found if:
				if( !Main.tileSolidTop[tile.type] && tile.type != TileID.MagicalIceBlock ) {
					return true;
				}
			}

			return false;
		}

		public static bool IsSuitableForFramingPlank( int tileX, int tileY, int dirX, int dirY ) {
			int max = AdventureModeConfig.Instance.MaxFramingPlankLength;
			if( max < 0 ) {
				return true;
			}

			int plankType = ModContent.TileType<FramingPlankTile>();

			for( int i = 1; i < max; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + (i * dirX), tileY + (i * dirY) );
				if( !tile.active() || !Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type] ) {
					break;
				}

				// Anchor found if:
				if( tile.type != plankType ) {
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
				return AdventureModeTile.IsSuitableForPlatform( i, j, -1 )
					|| AdventureModeTile.IsSuitableForPlatform( i, j, 1 );
			}
			if( type == ModContent.TileType<FramingPlankTile>() ) {
				return AdventureModeTile.IsSuitableForFramingPlank( i, j, -1, 0 )
					|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 1, 0 )
					|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, -1 )
					|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, 1 );
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