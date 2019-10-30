using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Tiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;


namespace AdventureMode.Items {
	internal class HouseFurnishingKitItem : ModItem {
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


		////

		public static void MakeHouse(
					Player player,
					IList<(ushort TileX, ushort TileY)> innerHouseSpace,
					IList<(ushort TileX, ushort TileY)> fullHouseSpace,
					int floorX,
					int floorY ) {
			(int x, int y) topLeft=(0,0), topRight=(0,0);
			int floorLeft=0, floorRight=0;
			(int x, int y) farTopLeft = (floorX - 512, floorY - 512);
			(int x, int y) farTopRight = (floorX + 512, floorY - 512);

			foreach( (ushort tileX, ushort tileY) in innerHouseSpace ) {
				Tile tile = Main.tile[tileX, tileY];

				if( topLeft.x == 0 ) {
					topLeft.x = topRight.x = tileX;
					topLeft.y = topRight.y = tileY;
				}

				if( tileY == (floorY-2) ) {
					if( floorLeft == 0 ) {
						floorLeft = floorRight = tileX;
					} else {
						if( floorLeft > tileX ) {
							floorLeft = tileX;
						} else if( floorRight < tileX ) {
							floorRight = tileX;
						}
					}
				}

				(int x, int y) oldTopLeft = (topLeft.x - farTopLeft.x, topLeft.y - farTopLeft.y);
				(int x, int y) newTopLeft = (tileX - farTopLeft.x, tileY - farTopLeft.y);

				int oldTopLeftDistSqr = ( oldTopLeft.x * oldTopLeft.x ) + ( oldTopLeft.y * oldTopLeft.y );
				int newTopLeftDistSqr = ( newTopLeft.x * newTopLeft.x ) + ( newTopLeft.y * newTopLeft.y );
				if( newTopLeftDistSqr < oldTopLeftDistSqr ) {
					topLeft.x = tileX;
					topLeft.y = tileY;
				}

				(int x, int y) oldTopRight = (topRight.x - farTopRight.x, topRight.y - farTopRight.y);
				(int x, int y) newTopRight = (tileX - farTopRight.x, tileY - farTopRight.y);

				int oldTopRightDistSqr = ( oldTopRight.x * oldTopRight.x ) + ( oldTopRight.y * oldTopRight.y );
				int newTopRightDistSqr = ( newTopRight.x * newTopRight.x ) + ( newTopRight.y * newTopRight.y );
				if( newTopRightDistSqr < oldTopRightDistSqr ) {
					topRight.x = tileX;
					topRight.y = tileY;
				}
			}

			foreach( (ushort tileX, ushort tileY) in fullHouseSpace ) {
				Tile tile = Main.tile[tileX, tileY];

				if( !Main.wallHouse[ tile.wall ] ) {
					WorldGen.PlaceWall( tileX, tileY, WallID.Wood, true );
				}
			}

			for( int i=floorLeft; i<floorLeft+4; i++ ) {
				for( int j=floorY-1; j<=floorY; j++ ) {
					Main.tile[i, j].active( false );
				}
			}
			WorldGen.Place4x2( floorLeft, floorY, TileID.Beds, 1, 0 );
			
			for( int i=floorRight-3; i<=floorRight; i++ ) {
				for( int j=floorY-1; j<=floorY; j++ ) {
					Main.tile[i, j].active( false );
				}
			}
			WorldGen.Place3x2( floorRight-1, floorY, TileID.Tables, 0 );

			for( int j = floorY-1; j <= floorY; j++ ) {
				Main.tile[floorRight-4, j].active( false );
			}
			WorldGen.Place1x2( floorRight-3, floorY, TileID.Chairs, 0 );
			Main.tile[ floorRight-3, floorY-1 ].frameX += 18;
			Main.tile[ floorRight-3, floorY ].frameX += 18;

			Main.tile[ topLeft.x-1, topLeft.y ].type = TileID.Torches;
			Main.tile[ topLeft.x-1, topLeft.y ].active( true );
			Main.tile[ topRight.x, topRight.y ].type = TileID.Torches;
			Main.tile[ topRight.x, topRight.y ].active( true );
		}



		////////////////

		public static int Width = 24;
		public static int Height = 22;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "House Furnishing Kit" );
			this.Tooltip.SetDefault( "Attempts to transform a given space into a livable area" );
		}

		public override void SetDefaults() {
			this.item.width = HouseFurnishingKitItem.Width;
			this.item.height = HouseFurnishingKitItem.Height;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			//this.item.UseSound = SoundID.Item108;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 2;
		}


		////////////////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation > 0 && player.itemTime == 0 ) {
				player.itemTime = item.useTime;
				return true;
			}
			return base.UseItem( player );
		}

		public override bool ConsumeItem( Player player ) {
			int tileX = (int)player.Center.X >> 4;
			int tileY = (int)player.Center.Y >> 4;
			IList<(ushort TileX, ushort TileY)> innerHouseSpace, fullHouseSpace;
			int floorY;

			if( HouseFurnishingKitItem.IsValidHouse( tileX, tileY, out innerHouseSpace, out fullHouseSpace, out floorY ) ) {
				HouseFurnishingKitItem.MakeHouse( player, innerHouseSpace, fullHouseSpace, tileX, floorY );
				return true;
			}

			return false;
		}
	}
}
