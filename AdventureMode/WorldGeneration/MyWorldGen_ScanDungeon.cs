using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static void ScanDungeon( GenerationProgress progress ) {
			var pattern = new TilePattern( new TilePatternBuilder {
				IsActive = true,
				IsAnyOfType = new HashSet<int> {
					TileID.BlueDungeonBrick,
					TileID.GreenDungeonBrick,
					TileID.PinkDungeonBrick
				}
			} );
			Rectangle? bounds = TileFinderHelpers.FindBoxForAllOf( pattern: pattern );

			var myworld = ModContent.GetInstance<AMWorld>();
			(int, int)? point = AMWorldGen.ScanForDungeonBottom( bounds.Value, out int scanCount );
			if( !point.HasValue ) {
				throw new ModHelpersException( "Could not locate viable bottom point within the dungeon "
					+"(scanned "+scanCount+" tiles within "+bounds.Value+")." );
			}

			myworld.DungeonBottom = point.Value;

			progress.Set( 1f );
		}


		private static (int tileX, int tileY)? ScanForDungeonBottom( Rectangle bounds, out int scanCount ) {
			var pattern = new TilePattern( new TilePatternBuilder {
				IsActive = false,
				IsAnyOfWallType = new HashSet<int> {
					WallID.BlueDungeonSlabUnsafe,
					WallID.BlueDungeonTileUnsafe,
					WallID.GreenDungeonSlabUnsafe,
					WallID.GreenDungeonTileUnsafe,
					WallID.PinkDungeonSlabUnsafe,
					WallID.PinkDungeonTileUnsafe,
				},
				//AreaFromCenter = new Rectangle(-1, -1, 3, 3)
			} );
			scanCount = 0;

			for( int y=bounds.Bottom; y>bounds.Top; y-- ) {
				for( int x=bounds.Left; x<bounds.Right; x++ ) {
					scanCount++;
					if( !pattern.Check(x, y) ) {
						continue;
					}

					return (x, y);
				}
			}

			return null;
		}
	}
}
