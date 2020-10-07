using System;
using System.Collections.Generic;
using HamstarHelpers.Helpers.Debug;
using TerrainRemixer;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			var terrainRemixerConfig = TerrainRemixerConfig.Instance;
			var list = new List<TerrainRemixerGenPassSpec> {
				new TerrainRemixerGenPassSpec {
					NoiseFrequency = 0.1f,
					WormsMode = FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.3f,
					VerticalDistancePercentFromCenterBeforeBlending = 0.7f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = WorldDepth.SkyTop,
					BoundsBottomStart = WorldDepth.UndergroundRockTop,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
				}
			};

			terrainRemixerConfig.SetOverride( nameof(TerrainRemixerConfig.Passes), list );
		}
	}
}
