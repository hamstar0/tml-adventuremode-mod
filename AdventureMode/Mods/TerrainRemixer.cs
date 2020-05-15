using System;
using HamstarHelpers.Helpers.Debug;
using TerrainRemixer;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			var config = new TerrainRemixerConfig {
				Passes = {
					new TerrainRemixerGenPassSpec {
						NoiseScale = 0.007f,
						NoiseValueMinimumUntilTileRemoval = 0.3f,
						VerticalDistancePercentFromCenterBeforeBlending = 0.7f,
						HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
						BoundsTopStart = WorldDepth.SkyTop,
						BoundsBottomStart = WorldDepth.UndergroundRockTop,
						BoundsLeftPercentStart = 0f,
						BoundsRightPercentStart = 1f,
					}
				}
			};

			TerrainRemixerConfig.Instance.OverlayChanges( config );
		}
	}
}
