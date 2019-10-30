using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Tiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;


namespace AdventureMode.Items {
	partial class HouseFurnishingKitItem : ModItem {
		public static bool IsValidHouse( int tileX, int tileY ) {
			IList<(ushort TileX, ushort TileY)> innerHouseSpace;
			IList<(ushort TileX, ushort TileY)> fullHouseSpace;
			int floorY;

			return HouseFurnishingKitItem.IsValidHouse( tileX, tileY, out innerHouseSpace, out fullHouseSpace, out floorY );
		}


		public static bool IsValidHouse(
					int tileX,
					int tileY,
					out IList<(ushort TileX, ushort TileY)> innerHouseSpace,
					out IList<(ushort TileX, ushort TileY)> fullHouseSpace,
					out int floorY ) {
			bool customCheck( int x, int y ) {
				Tile tile = Main.tile[x, y];
				return !tile.active()
					|| ( !Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type] )  //non-solid, non-platform
					|| ( Main.tileSolid[tile.type] && Main.tileSolidTop[tile.type] && tile.slope() != 0 );  //stair
			}

			TilePattern pattern1 = new TilePattern( new TilePatternBuilder {
				AreaFromCenter = new Rectangle( -2, -2, 4, 4 ),
				HasWater = false,
				HasHoney = false,
				HasLava = false,
				IsActuated = false,
				CustomCheck = customCheck
			} );
			TilePattern pattern2 = new TilePattern( new TilePatternBuilder {
				HasWater = false,
				HasHoney = false,
				HasLava = false,
				IsActuated = false,
				CustomCheck = customCheck
			} );

			if( !HouseFurnishingKitItem.IsValidHouseByCriteria( pattern1, tileX, tileY, out innerHouseSpace, out floorY ) ) {
				fullHouseSpace = innerHouseSpace;
				return false;
			}

			if( !HouseFurnishingKitItem.IsValidHouseByCriteria( pattern2, tileX, tileY, out fullHouseSpace, out floorY ) ) {
				return false;
			}

			return true;
		}


		private static bool IsValidHouseByCriteria(
					TilePattern pattern,
					int tileX,
					int tileY,
					out IList<(ushort TileX, ushort TileY)> houseSpace,
					out int floorY ) {
			IList<(ushort TileX, ushort TileY)> unclosedTiles;
			houseSpace = TileFinderHelpers.GetAllContiguousMatchingTiles(
				pattern,
				tileX,
				tileY,
				out unclosedTiles,
				64
			);

			if( unclosedTiles.Count > 0 ) {
				Main.NewText( "House too large or not a closed space.", Color.Yellow );
				floorY = 0;
				return false;
			}

			if( houseSpace.Count < 80 ) {
				Main.NewText( "House too small.", Color.Yellow );
				floorY = 0;
				return false;
			}

			int floorWidth = TileFinderHelpers.GetFloorWidth( pattern, tileX, tileY - 2, 32, out floorY );
			if( floorWidth < 10 ) {
				Main.NewText( "Not enough floor space.", Color.Yellow );
				return false;
			}

			return true;
		}
	}
}
