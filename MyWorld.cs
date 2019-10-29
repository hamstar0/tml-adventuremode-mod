using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Tiles;
using HamstarHelpers.Classes.Tiles.TilePattern;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		internal static void InitializeSingleton() {
			var myworld = ModContent.GetInstance<AdventureModeWorld>();

			myworld.ManaCrystalShardPattern = new TilePattern( new TilePatternBuilder {
				IsSolid = false,
				IsActuated = false,
				IsPlatform = false,
				HasLava = false,
				HasWire1 = false,
				HasWire2 = false,
				HasWire3 = false,
				HasWire4 = false,
				MaximumBrightness = 0.05f,
				CustomCheck = ( x, y ) => {
					if( Main.tile[x, y].type == ModContent.TileType<ManaCrystalShardTile>() ) {
						return false;
					}
					return ManaCrystalShardTile.PredictFrameY( x, y ) != -1;
				}
			} );
		}



		////////////////

		private IDictionary<int, int> ManaCrystalShardChecked = new Dictionary<int, int>();	// Technically incorrect; unimportant

		private (int tileX, int tileY)[] ManaCrystalShardCheckQueue = new (int, int)[1];
		private int ManaCrystalShardCheckQueueSize = 0;


		////////////////

		public TilePattern ManaCrystalShardPattern { get; private set; }



		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int shards;

			switch( WorldHelpers.GetSize() ) {
			default:
			case WorldSize.SubSmall:
				shards = AdventureModeMod.Config.TinyWorldManaCrystalShards;
				break;
			case WorldSize.Small:
				shards = AdventureModeMod.Config.SmallWorldManaCrystalShards;
				break;
			case WorldSize.Medium:
				shards = AdventureModeMod.Config.MediumWorldManaCrystalShards;
				break;
			case WorldSize.Large:
				shards = AdventureModeMod.Config.LargeWorldManaCrystalShards;
				break;
			case WorldSize.SuperLarge:
				shards = AdventureModeMod.Config.HugeWorldManaCrystalShards;
				break;
			}

			tasks.Add( new AdventureModeWorldGenPass( shards ) );
		}


		////////////////

		public override void PreUpdate() {
			if( this.ManaCrystalShardCheckQueueSize > 0 ) {
				this.ProcessManaCrystalShardQueue();
			}
		}


		////////////////

		public void QueueManaCrystalShardCheck( int tileX, int tileY, float brightness ) {
			if( Main.netMode == 1 ) {
				ManaCrystalShardCheckProtocol.QuickRequest( tileX, tileY, brightness );
			} else {
				if( this.ManaCrystalShardCheckQueueSize == (this.ManaCrystalShardCheckQueue.Length - 1) ) {
					Array.Resize( ref this.ManaCrystalShardCheckQueue, this.ManaCrystalShardCheckQueue.Length * 2 );
				}
				this.ManaCrystalShardCheckQueue[ this.ManaCrystalShardCheckQueueSize++ ] = (tileX, tileY);
			}
		}

		private void ProcessManaCrystalShardQueue() {
			int shardType = ModContent.TileType<ManaCrystalShardTile>();

			for( int i = 0; i < this.ManaCrystalShardCheckQueueSize; i++ ) {
				(int tileX, int tileY) tileAt = this.ManaCrystalShardCheckQueue[i];

				if( this.ManaCrystalShardChecked.ContainsKey( tileAt.tileX ) && this.ManaCrystalShardChecked[tileAt.tileX] == tileAt.tileY ) {
					continue;
				}
				this.ManaCrystalShardChecked[tileAt.tileX] = tileAt.tileY;

				Tile tile = Framing.GetTileSafely( tileAt.tileX, tileAt.tileY );
				float brightness = Lighting.Brightness( tileAt.tileX, tileAt.tileY );

				if( tile.active() && tile.type == shardType ) {
					ManaCrystalShardTile.UpdateLightAversionForTile(
						tileAt.tileX,
						tileAt.tileY,
						(float)AdventureModeMod.Config.ManaCrystalShardLightToleranceScale,
						brightness
					);
				}
			}

			this.ManaCrystalShardChecked.Clear();
			this.ManaCrystalShardCheckQueueSize = 0;
		}
	}
}
