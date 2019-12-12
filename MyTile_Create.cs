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

		public static bool IsSuitableForRope( int tileX, int tileY ) {
			Tile tileBelow = Framing.GetTileSafely( tileX, tileY + 1 );

			bool belowIsNotRope = tileBelow.type != TileID.Rope
				&& tileBelow.type != TileID.SilkRope
				&& tileBelow.type != TileID.VineRope
				&& tileBelow.type != TileID.WebRope;
			if( belowIsNotRope ) {
				return true;
			}

			Tile tile = Framing.GetTileSafely( tileX, tileY );
			bool hereIsNotRope = tile.type != TileID.Rope
				&& tile.type != TileID.SilkRope
				&& tile.type != TileID.VineRope
				&& tile.type != TileID.WebRope;

			if( hereIsNotRope ) {
				Tile tileAbove = Framing.GetTileSafely( tileX, tileY - 1 );

				return tileAbove.type != TileID.Rope
					&& tileAbove.type != TileID.SilkRope
					&& tileAbove.type != TileID.VineRope
					&& tileAbove.type != TileID.WebRope;
			}

			return true;
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
			// World gen?
			if( Main.gameMenu || !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			switch( type ) {
			case TileID.Platforms:
				return AdventureModeTile.IsSuitableForPlatform( i, j, -1 )
					|| AdventureModeTile.IsSuitableForPlatform( i, j, 1 );
			case TileID.Rope:
			case TileID.SilkRope:
			case TileID.VineRope:
			case TileID.WebRope:
				return AdventureModeTile.IsSuitableForRope( i, j );
			default:
				if( type == ModContent.TileType<FramingPlankTile>() ) {
					return AdventureModeTile.IsSuitableForFramingPlank( i, j, -1, 0 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 1, 0 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, -1 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, 1 );
				}
				if( type == ModContent.TileType<MountedMagicMirrorTile>() ) {
					return true;
				}
				if( AdventureModeConfig.Instance.TilePlaceWhitelist.Contains( TileID.GetUniqueKey( type ) ) ) {
					return true;
				}
				break;
			}

			return false;
		}
	}
}