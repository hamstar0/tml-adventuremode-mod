using System;
using System.Collections.Generic;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using TerrainRemixer;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadTerrainRemixer() {
			AdventureModeModInteractions._LoadTerrainRemixer_WeakRef();
		}

		private static void _LoadTerrainRemixer_WeakRef() {
			var terrainRemixerConfig = TerrainRemixer.TerrainRemixerConfig.Instance;

			// Clear existing default passes
			if( !terrainRemixerConfig.HasOverride("Passes") ) {
				terrainRemixerConfig.SetOverride( "Passes", new List<TerrainRemixerGenPassSpec>() );
			}

			TerrainRemixer.TerrainRemixerAPI.AddPassHook(
				() => {
					// Upper caves softening
					return new TerrainRemixer.TerrainRemixerGenPassSpec {
						PassName = "High Cave Opener",
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
					};
				}
			);
			TerrainRemixer.TerrainRemixerAPI.AddPassHook(
				() => {
					// Deep caves blocking
					return new TerrainRemixer.TerrainRemixerGenPassSpec {
						PassName = "Deep Cave Closer",
						LayerName = "Grass",
						NoiseFrequency = 0.0075f,   //0.01f
						WormsMode = TerrainRemixer.FractalType.Billow,
						Sharpness = 0.3f,
						NoiseValueMinimumUntilTileRemoval = 0.2f,   //was 0.175f
						VerticalDistancePercentFromCenterBeforeBlending = 0.8f,
						HorizontalDistancePercentFromCenterBeforeBlending = 0.9f,
						BoundsTopStart = TerrainRemixer.WorldDepth.UndergroundDirtTop,
						BoundsBottomStart = TerrainRemixer.WorldDepth.Bottom,
						BoundsLeftPercentStart = 0f,
						BoundsRightPercentStart = 1f,
						FillTiles = new List<int> { TileID.Stone }
					};
				}
			);
			TerrainRemixer.TerrainRemixerAPI.AddPassHook(
				() => {
					float centerPercentOffset = 0.075f;	// was 0.1f

					switch( WorldLibraries.GetSize() ) {
					case WorldSize.SubSmall:
					case WorldSize.Small:
						centerPercentOffset = 0.085f;
						break;
					case WorldSize.Medium:
						centerPercentOffset = 0.06f;
						break;
					case WorldSize.Large:
						centerPercentOffset = 0.045f;
						break;
					case WorldSize.SuperLarge:
					default:
						centerPercentOffset = 0.03f;
						break;
					}

					// Extreme hills in map center
					return new TerrainRemixer.TerrainRemixerGenPassSpec {
						PassName = "Mid 'Extreme Hills'",
						LayerName = "Tunnels",
						NoiseFrequency = 0.01f,
						WormsMode = TerrainRemixer.FractalType.FBM,
						NoiseValueMinimumUntilTileRemoval = 0.38f, //lower is thinner, was 0.36
						BoundsTopStart = TerrainRemixer.WorldDepth.SkyTop,
						BoundsBottomStart = TerrainRemixer.WorldDepth.UnderworldTop,
						HorizontalDistancePercentFromCenterBeforeBlending = 0.8f,
						VerticalDistancePercentFromCenterBeforeBlending = 0.5f, //was 0.7
						BoundsLeftPercentStart = 0.5f - centerPercentOffset,
						BoundsRightPercentStart = 0.5f + centerPercentOffset,
						FillTiles = new List<int> { -1 },
						FillWalls = new List<int> { }
					};
				}
			);
		}
	}
}
