﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsTiles.Libraries.Tiles;
using ModLibsTiles.Classes.Tiles.TilePattern;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public static void ScanUndergroundDesert( GenerationProgress progress ) {
			ISet<Rectangle> boxes = TileFinderLibraries.FindBoxesOfAllContiguousMatches(
				pattern: new TilePattern( new TilePatternBuilder {
					IsAnyOfWallType = new HashSet<int> { WallID.HardenedSand, WallID.Sandstone }
				} ),
				progress: progress,
				skip: 32
			);

			if( boxes.Count > 0 ) {
				Rectangle box = boxes
					.Aggregate( (b1, b2) => (b1.Width * b1.Height) > (b2.Width * b2.Height) ? b1 : b2 );

				var myworld = ModContent.GetInstance<AMWorld>();
				myworld.UndergroundDesertBounds = box;

				if( AMConfig.Instance.DebugModeInfo ) {
					LogLibraries.Log( "Underground desert occupies tile range " + box.ToString() );
				}
			}

			progress.Set( 1f );
		}
	}
}
