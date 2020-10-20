﻿using System;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Classes.Tiles.TilePattern;


namespace AdventureMode.WorldGeneration {
	partial class AdventureModeWorldGen {
		public static void ScanDungeon( GenerationProgress progress ) {
			var pattern = new TilePattern( new TilePatternBuilder {
				IsAnyOfType = new HashSet<int> { TileID.BlueDungeonBrick, TileID.GreenDungeonBrick, TileID.PinkDungeonBrick }
			} );
			Rectangle? bounds = TileFinderHelpers.FindBoxForAllOf( pattern: pattern );

			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			(int, int)? point = AdventureModeWorldGen.ScanForDungeonBottom( bounds.Value );
			myworld.DungeonBottom = point.Value;

			progress.Set( 1f );
		}


		private static (int tileX, int tileY)? ScanForDungeonBottom( Rectangle bounds ) {
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
				AreaFromCenter = new Rectangle(-1, 1, 3, 3)
			} );

			for( int y=bounds.Bottom; y>bounds.Top; y-- ) {
				for( int x=bounds.Left; x>bounds.Right; x++ ) {
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
