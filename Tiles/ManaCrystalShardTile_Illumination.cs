using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Tiles {
	partial class ManaCrystalShardTile : ModTile {
		public void IlluminateAt( int i, int j ) {
			var singleton = ModContent.GetInstance<ManaCrystalShardTile>();
			if( !singleton.IlluminatedCrystals.ContainsKey( i ) || !singleton.IlluminatedCrystals[i].ContainsKey( j ) ) {
				LogHelpers.Warn( "Cannot illuminate "+i+","+j+"; no shard defined");
				return;
			}

			if( Main.tile[i,j].type != ModContent.TileType<ManaCrystalShardTile>() ) {
				LogHelpers.Warn( "Cannot illuminate "+i+","+j+"; incorrect tile");
				return;
			}

			singleton.IlluminatedCrystals[i][j] = 1f;
		}


		////

		private float UpdateIlluminationAt( int i, int j ) {
			var singleton = ModContent.GetInstance<ManaCrystalShardTile>();
			if( !singleton.IlluminatedCrystals.ContainsKey( i ) || !singleton.IlluminatedCrystals[i].ContainsKey( j ) ) {
				singleton.IlluminatedCrystals.Set2D( i, j, 0f );
				return 0f;
			}

			float illum = singleton.IlluminatedCrystals[i][j];

			if( illum > 0f ) {
				illum = singleton.IlluminatedCrystals[i][j] -= 0.1f;
			}

			// Animate flicker
			if( ((int)(illum * 10) % 2) == 0 ) {
				return 0f;
			}

			return illum;
		}
	}
}
