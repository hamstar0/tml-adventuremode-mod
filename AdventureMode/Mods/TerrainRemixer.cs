using System;
using System.Collections.Generic;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using TerrainRemixer;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			var terrainRemixerConfig = TerrainRemixerConfig.Instance;
			var list = new List<TerrainRemixerGenPassSpec> {
				new TerrainRemixerGenPassSpec {
					LayerName = "Tunnels",
					NoiseFrequency = 0.005f,	//0.01f
					WormsMode = FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.175f,	//0.3f
					VerticalDistancePercentFromCenterBeforeBlending = 0.7f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = WorldDepth.SkyTop,
					BoundsBottomStart = WorldDepth.UnderworldTop,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					BoundsBottomPercentStart = 0.75f,
					FillTiles = new List<int> { -1 }
				},
				new TerrainRemixerGenPassSpec {
					LayerName = "Grass",
					NoiseFrequency = 0.0075f,	//0.01f
					WormsMode = FractalType.Billow,
					Sharpness = 0.3f,
					NoiseValueMinimumUntilTileRemoval = 0.175f,	//0.3f
					VerticalDistancePercentFromCenterBeforeBlending = 0.8f,
					HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
					BoundsTopStart = WorldDepth.UndergroundDirtTop,
					BoundsBottomStart = WorldDepth.Bottom,
					BoundsLeftPercentStart = 0f,
					BoundsRightPercentStart = 1f,
					FillTiles = new List<int> { TileID.Stone }
				}
			};

			terrainRemixerConfig.SetOverride( nameof(TerrainRemixerConfig.Passes), list );
		}
	}
}
