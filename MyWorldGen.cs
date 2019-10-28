using HamstarHelpers.Classes.Tiles.TilePattern;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Tiles;


namespace AdventureMode {
	class AdventureModeGenPass : GenPass {
		private TilePattern ManaCrystalPattern;
		private int NeededShards;



		////////////////

		public AdventureModeGenPass( int shards ) : base( "PopulateManaCrystalShards", 1f ) {
			this.NeededShards = shards;

			this.ManaCrystalPattern = new TilePattern( new TilePatternBuilder {
				IsSolid = false,
				IsActuated = false,
				IsPlatform = false,
				HasLava = false,
				HasWire1 = false,
				HasWire2 = false,
				HasWire3 = false,
				HasWire4 = false,
				CustomCheck = ( x, y ) => {
					return ManaCrystalShardTile.PredictFrameY( x, y ) != -1;
				}
			} );
		}


		////////////////

		public override void Apply( GenerationProgress progress ) {
			(int TileX, int TileY) randCenterTile;
			float stepWeight = 1f / (float)this.NeededShards;

			if( progress != null ) {
				progress.Message = "Pre-placing Mana Crystal Shards: %";
			}

			for( int i = 0; i < this.NeededShards; i++ ) {
				progress?.Set( stepWeight * (float)i );

				if( !this.GetRandomOpenMirrorableCenterTile(out randCenterTile, 10000) ) {
					break;
				}

				this.SpawnShard( randCenterTile.TileX, randCenterTile.TileY );
			}
		}


		////////////////

		private bool GetRandomOpenMirrorableCenterTile( out (int TileX, int TileY) randTileCenter, int maxAttempts ) {
			int attempts = 0;

			do {
				randTileCenter = this.GetRandomMirrorableCenterTile( maxAttempts );
			} while( attempts++ < maxAttempts );

			return false;
		}


		private (int TileX, int TileY) GetRandomMirrorableCenterTile( int maxAttempts ) {
			int attempts = 0;
			int randCaveTileX, randCaveTileY;

			do {
				randCaveTileX = WorldGen.genRand.Next( 64, Main.maxTilesX - 64 );
				randCaveTileY = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 220 );

				if( this.ManaCrystalPattern.Check( randCaveTileX, randCaveTileY ) ) {
					break;
				}
			} while( attempts++ < maxAttempts );

			return (randCaveTileX, randCaveTileY);
		}


		////////////////

		private void SpawnShard( int centerTileX, int centerTileY ) {
			ushort shardTile = (ushort)ModContent.TileType<ManaCrystalShardTile>();

			WorldGen.Place1x1( centerTileX, centerTileY, shardTile, 0 );

			if( AdventureModeMod.Config.DebugModeInfo ) {
				LogHelpers.Log( "Placed Mana Crystal Shard (of " + this.NeededShards + ")" +
					" at " + centerTileX + "," + centerTileY +
					" (" + ( centerTileX << 4 ) + "," + ( centerTileY << 4 ) + ")"
				);
			}
		}
	}
}
