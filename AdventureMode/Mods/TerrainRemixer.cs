using System;
using System.Collections.Generic;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			var terrainRemixerConfig = TerrainRemixer.TerrainRemixerConfig.Instance;
			var list = new List<TerrainRemixer.TerrainRemixerGenPassSpec> {
				// Upper caves softening
				new TerrainRemixer.TerrainRemixerGenPassSpec {
					LayerName = "Tunnels",
					NoiseFrequency = 0.005f,
					WormsMode = TerrainRemixer.FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.175f,
					VerticalDistancePercentFromCenterBeforeBlending = 0.7f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = TerrainRemixer.WorldDepth.SkyTop,
					BoundsBottomStart = TerrainRemixer.WorldDepth.UnderworldTop,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					BoundsBottomPercentStart = 0.75f,
					FillTiles = new List<int> { -1 }
				},
				// Deep caves blocking
				new TerrainRemixer.TerrainRemixerGenPassSpec {
					LayerName = "Grass",
					NoiseFrequency = 0.0075f,	//0.01f
					WormsMode = TerrainRemixer.FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.2f,	//was 0.175f
					VerticalDistancePercentFromCenterBeforeBlending = 0.8f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = TerrainRemixer.WorldDepth.UndergroundDirtTop,
					BoundsBottomStart = TerrainRemixer.WorldDepth.Bottom,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					FillTiles = new List<int> { TileID.Stone }
				},
				// Extreme hills in map center
				new TerrainRemixer.TerrainRemixerGenPassSpec {
					LayerName = "Tunnels",
					NoiseFrequency = 0.01f,
					WormsMode = TerrainRemixer.FractalType.FBM,
					NoiseValueMinimumUntilTileRemoval = 0.38f, //lower is thinner, was 0.36
					BoundsTopStart = TerrainRemixer.WorldDepth.SkyTop,
					BoundsBottomStart = TerrainRemixer.WorldDepth.UnderworldTop,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.8f,
					VerticalDistancePercentFromCenterBeforeBlending = 0.5f,	//was 0.7
					BoundsLeftPercentStart = 0.425f,	//was 0.4
					BoundsRightPercentStart = 0.575f,	//was 0.6
					FillTiles = new List<int> { -1 },
					FillWalls = new List<int> { }
				},
			};

			terrainRemixerConfig.SetOverride(
				nameof(TerrainRemixer.TerrainRemixerConfig.Passes),
				list
			);
		}
	}
}
