using System;
using System.Collections.Generic;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using TerrainRemixer;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			var terrainRemixerConfig = TerrainRemixerConfig.Instance;
			var list = new List<TerrainRemixerGenPassSpec> {
				// Upper caves softening
				new TerrainRemixerGenPassSpec {
					LayerName = "Tunnels",
					NoiseFrequency = 0.005f,
					WormsMode = FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.175f,
					VerticalDistancePercentFromCenterBeforeBlending = 0.7f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = WorldDepth.SkyTop,
					BoundsBottomStart = WorldDepth.UnderworldTop,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					BoundsBottomPercentStart = 0.75f,
					FillTiles = new List<int> { -1 }
				},
				// Deep caves blocking
				new TerrainRemixerGenPassSpec {
					LayerName = "Grass",
					NoiseFrequency = 0.0075f,	//0.01f
					WormsMode = FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.2f,	//was 0.175f
					VerticalDistancePercentFromCenterBeforeBlending = 0.8f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = WorldDepth.UndergroundDirtTop,
					BoundsBottomStart = WorldDepth.Bottom,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					FillTiles = new List<int> { TileID.Stone }
				},
				// Extreme hills in map center
				new TerrainRemixerGenPassSpec {
					LayerName = "Tunnels",
					NoiseFrequency = 0.01f,
					WormsMode = FractalType.FBM,
					NoiseValueMinimumUntilTileRemoval = 0.38f, //lower is thinner, was 0.36
					BoundsTopStart = WorldDepth.SkyTop,
					BoundsBottomStart = WorldDepth.UnderworldTop,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.8f,
					VerticalDistancePercentFromCenterBeforeBlending = 0.5f,	//was 0.7
					BoundsLeftPercentStart = 0.425f,	//was 0.4
					BoundsRightPercentStart = 0.575f,	//was 0.6
					FillTiles = new List<int> { -1 },
					FillWalls = new List<int> { }
				},
			};

			terrainRemixerConfig.SetOverride( nameof(TerrainRemixerConfig.Passes), list );
		}
	}
}
