using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Tiles;


namespace AdventureMode.Logic {
	static partial class TileLogic {
		public static bool IsSuitableForPlatform( int tileX, int tileY, int dirX ) {
			int max = AdventureModeConfig.Instance.MaxPlatformBridgeLength;
			if( max < 0 ) {
				return true;
			}

			for( int i = 1; i <= max; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + ( i * dirX ), tileY );
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
				return isRopeTile( tile );
			}
			bool isRopeTile( Tile tile ) {
				return tile.type == TileID.Rope
					|| tile.type == TileID.SilkRope
					|| tile.type == TileID.VineRope
					|| tile.type == TileID.WebRope
					|| tile.type == TileID.Chain;
			}

			//

			if( Framing.GetTileSafely( tileX, tileY ).wall != 0 ) {
				return true;
			}

			if( isRope( tileX, tileY ) ) {
				return true;
			}

			if( !isRope( tileX, tileY + 1 ) ) {
				return true;
			}

			if( Framing.GetTileSafely( tileX, tileY - 1 ).active() ) {  //isRope(tileX, tileY-1) ) {
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
			if( isTrack( tileX - 1, tileY - 1 ) || isTrack( tileX - 1, tileY ) || isTrack( tileX - 1, tileY + 1 ) ) {

				for( int i = 1; i < maxGapCheck; i++ ) {
					if( isTrack( tileX + i, tileY - 1 ) || isTrack( tileX + i, tileY ) || isTrack( tileX + i, tileY + 1 ) ) {
						foundGap = true;
						break;
					}
				}
			}
			if( foundGap ) { return true; }

			// Allow patching holes from the right
			if( isTrack( tileX + 1, tileY - 1 ) || isTrack( tileX + 1, tileY ) || isTrack( tileX + 1, tileY + 1 ) ) {
				for( int i = 1; i < maxGapCheck; i++ ) {
					if( isTrack( tileX - i, tileY - 1 ) || isTrack( tileX - i, tileY ) || isTrack( tileX - i, tileY + 1 ) ) {
						foundGap = true;
						break;
					}
				}
			}
			if( foundGap ) { return true; }

			// Otherwise, only downward placement
			if( isTrack( tileX - 1, tileY - 1 ) || isTrack( tileX + 1, tileY - 1 ) ) {
				return true;
			}

			return false;
		}

		public static bool IsSuitableForFramingPlank( int tileX, int tileY, int dirX, int dirY ) {
			int max = dirY != 0
				? AdventureModeConfig.Instance.MaxFramingPlankVerticalLength
				: AdventureModeConfig.Instance.MaxFramingPlankHorizontalLength;
			if( max < 0 ) {
				return true;
			}

			int plankType = ModContent.TileType<FramingPlankTile>();

			for( int i = 1; i <= max; i++ ) {
				Tile tile = Framing.GetTileSafely( tileX + ( i * dirX ), tileY + ( i * dirY ) );
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

		public static bool CanPlacePlatform( int i, int j ) {
			return TileLogic.IsSuitableForPlatform( i, j, -1 )
				|| TileLogic.IsSuitableForPlatform( i, j, 1 );
		}

		public static bool CanPlaceRope( int i, int j ) {
			return TileLogic.IsSuitableForRope( i, j );
		}

		public static bool CanPlaceTrack( int i, int j ) {
			return TileLogic.IsSuitableForTrack( i, j );
		}

		public static bool CanPlaceFramingPlank( int i, int j ) {
			return TileLogic.IsSuitableForFramingPlank( i, j, -1, 0 )
				|| TileLogic.IsSuitableForFramingPlank( i, j, 1, 0 )
				|| TileLogic.IsSuitableForFramingPlank( i, j, 0, -1 )
				|| TileLogic.IsSuitableForFramingPlank( i, j, 0, 1 );
		}

		public static bool CanPlaceOther( int i, int j, int type ) {
			if( AdventureModeConfig.Instance.TilePlaceWhitelist.Contains( TileID.GetUniqueKey( type ) ) ) {
				return true;
			}

			return false;
		}
	}
}
