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
			bool isRope( int x, int y ) {
				Tile tile = Framing.GetTileSafely( x, y );
				return tile.type == TileID.Rope
					|| tile.type == TileID.SilkRope
					|| tile.type == TileID.VineRope
					|| tile.type == TileID.WebRope
					|| tile.type == TileID.Chain;
			}

			if( Framing.GetTileSafely(tileX, tileY).wall != 0 ) {
				return true;
			}

			if( isRope(tileX, tileY) ) {
				return true;
			}

			if( !isRope(tileX, tileY+1) ) {
				return true;
			}

			if( isRope(tileX, tileY-1) ) {
				return true;
			}

			return false;
		}

		public static bool IsSuitableForTrack( int tileX, int tileY ) {
			bool isTrack( int x, int y ) {
				if( x < 0 || x >= Main.maxTilesX ) {
					return false;
				}
				if( y < 0 || y >= Main.maxTilesY ) {
					return false;
				}
				Tile tile = Framing.GetTileSafely( x, y );
				return tile.type == TileID.MinecartTrack;
			}

			bool foundGap = false;
			int maxGapCheck = AdventureModeConfig.Instance.MaxTrackGapPatchWidth;
			if( maxGapCheck == -1 ) {
				return true;
			}

			// Allow patching holes from the left
			if( isTrack(tileX-1, tileY-1) || isTrack(tileX-1, tileY) || isTrack(tileX-1, tileY+1) ) {

				for( int i=1; i<maxGapCheck; i++ ) {
					if( isTrack(tileX+i, tileY-1) || isTrack(tileX+i, tileY) || isTrack(tileX+i, tileY+1) ) {
						foundGap = true;
						break;
					}
				}
			}
			if( foundGap ) { return true; }

			// Allow patching holes from the right
			if( isTrack( tileX+1, tileY-1) || isTrack(tileX+1, tileY) || isTrack(tileX+1, tileY+1) ) {
				for( int i=1; i<maxGapCheck; i++ ) {
					if( isTrack(tileX-i, tileY-1) || isTrack(tileX-i, tileY) || isTrack(tileX-i, tileY+1) ) {
						foundGap = true;
						break;
					}
				}
			}
			if( foundGap ) { return true; }

			// Otherwise, only downward placement
			if( isTrack(tileX-1, tileY-1) || isTrack(tileX+1, tileY-1) ) {
				return true;
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
			case TileID.MinecartTrack:
				return AdventureModeTile.IsSuitableForTrack( i, j );
			default:
				if( type == ModContent.TileType<FramingPlankTile>() ) {
					return AdventureModeTile.IsSuitableForFramingPlank( i, j, -1, 0 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 1, 0 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, -1 )
						|| AdventureModeTile.IsSuitableForFramingPlank( i, j, 0, 1 );
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