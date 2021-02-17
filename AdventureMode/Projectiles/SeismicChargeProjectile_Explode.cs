using System;
using HamstarHelpers.Helpers.Fx;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Projectiles {
	public partial class SeismicChargeProjectile : ModProjectile {
		public static void CreateExplosion( int tileX, int tileY ) {
			int radius = 7;
			int radiusSqr = radius * radius;
			int left = tileX - radius;
			int right = tileX + radius;
			int top = tileY - radius;
			int bot = tileY + radius;

			for( int i=left; i<right; i++ ) {
				for( int j=top; j<bot; j++ ) {
					int xDiff = i - tileX;
					int yDiff = j - tileY;
					int distSqr = ((xDiff * xDiff) + (yDiff * yDiff));

					if( distSqr < radiusSqr ) {
						SeismicChargeProjectile.ProcessTileUnsynced( i, j, (float)Math.Sqrt( distSqr ), radius );
					}
				}
			}

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendTileSquare( -1, left, top, radius * 2 );
			}
		}


		public static void ProcessTileUnsynced( int tileX, int tileY, float dist, int maxDist ) {
			if( !WorldGen.InWorld(tileX, tileY) ) {
				return;
			}

			Tile tile = Main.tile[ tileX, tileY ];
			if( !tile.active() ) {
				return;
			}

			float distPerc = (float)dist / (float)maxDist;
			if( Main.rand.NextFloat() < distPerc ) {
				return;
			}

			ushort newTileType = tile.type;

			switch( tile.type ) {
			// Sandstone
			case TileID.Sandstone:
				newTileType = TileID.Sand;
				break;
			case TileID.CorruptSandstone:
				newTileType = TileID.Ebonsand;
				break;
			case TileID.CrimsonSandstone:
				newTileType = TileID.Crimsand;
				break;
			case TileID.HallowSandstone:
				newTileType = TileID.Pearlsand;
				break;
			// Hardened sand
			case TileID.HardenedSand:
				newTileType = TileID.Sandstone;
				break;
			case TileID.CorruptHardenedSand:
				newTileType = TileID.CorruptSandstone;
				break;
			case TileID.CrimsonHardenedSand:
				newTileType = TileID.CrimsonSandstone;
				break;
			case TileID.HallowHardenedSand:
				newTileType = TileID.HallowSandstone;
				break;
			// Ice
			case TileID.IceBlock:
			case TileID.CorruptIce:
			case TileID.FleshIce:
			case TileID.HallowedIce:
				newTileType = TileID.Slush;
				break;
			// Mud
			case TileID.Mud:
				newTileType = TileID.Silt;
				break;
			}

			tile.type = newTileType;

			ParticleFxHelpers.MakeDustCloud( new Vector2((tileX*16) + 8, (tileY*16) + 8), 1, 0.3f, 0.65f );

			WorldGen.SquareTileFrame( tileX, tileY, true );
		}
	}
}
