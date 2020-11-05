using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Classes.Tiles.TilePattern;
using System.Collections.Generic;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static void ScanUndergroundDesert( GenerationProgress progress ) {
			Rectangle? bounds = TileFinderHelpers.FindBoxForAllOf(
				pattern: new TilePattern( new TilePatternBuilder {
					IsAnyOfType = new HashSet<int> { TileID.HardenedSand, TileID.Sandstone }
				} )
			);

			var myworld = ModContent.GetInstance<AMWorld>();
			myworld.UndergroundDesertBounds = bounds.Value;

			progress.Set( 1f );
		}
	}
}
