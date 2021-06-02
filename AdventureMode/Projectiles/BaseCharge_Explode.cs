using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Fx;


namespace AdventureMode.Projectiles {
	public abstract partial class BaseExplosiveChargeProjectile : ModProjectile {
		public void CreateExplosion( int tileX, int tileY ) {
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
						this.ProcessTileUnsynced(
							tileX: i,
							tileY: j,
							dist: (float)Math.Sqrt( distSqr ),
							maxDist: radius
						);
					}
				}
			}

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendTileSquare( -1, left, top, radius * 2 );
			}
		}


		public void ProcessTileUnsynced( int tileX, int tileY, float dist, int maxDist ) {
			if( !WorldGen.InWorld(tileX, tileY) ) {
				return;
			}

			Tile tile = Main.tile[ tileX, tileY ];
			if( !tile.active() ) {
				return;
			}

			//float distPerc = (float)dist / (float)maxDist;
			//if( Main.rand.NextFloat() < distPerc ) {
			if( dist > maxDist ) {
				return;
			}

			tile.type = this.GetReplacementTileType( tile.type )
				?? tile.type;

			ParticleFxLibraries.MakeDustCloud( new Vector2((tileX*16) + 8, (tileY*16) + 8), 1, 0.3f, 0.65f );

			WorldGen.SquareTileFrame( tileX, tileY, true );
		}


		////////////////

		public abstract ushort? GetReplacementTileType( int tileType );
	}
}
