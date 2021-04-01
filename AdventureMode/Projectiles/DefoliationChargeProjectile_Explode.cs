using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Fx;


namespace AdventureMode.Projectiles {
	public partial class DefoliationChargeProjectile : ModProjectile {
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
						DefoliationChargeProjectile.ProcessTileUnsynced( i, j, (float)Math.Sqrt( distSqr ), radius );
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

			ushort? newTileType = tile.type;

			switch( tile.type ) {
			case TileID.VineFlowers:
			case TileID.Vines:
			case TileID.CrimsonVines:
			case TileID.HallowedVines:
			case TileID.JungleVines:
			case TileID.FleshWeeds:
			case TileID.CorruptPlants:
			case TileID.PlantDetritus:
			case TileID.HallowedPlants:
			case TileID.HallowedPlants2:
			case TileID.LongMoss:
			case TileID.JunglePlants:
			case TileID.JunglePlants2:
			case TileID.Plants:
			case TileID.Plants2:
			case TileID.ImmatureHerbs:
			case TileID.MatureHerbs:
			case TileID.BloomingHerbs:
			case TileID.Trees:
			case TileID.MushroomTrees:
			case TileID.PalmTree:
				newTileType = null;
				break;
			case TileID.Grass:
			case TileID.CorruptGrass:
			case TileID.FleshGrass:
			case TileID.HallowedGrass:
				newTileType = TileID.Dirt;
				break;
			case TileID.JungleGrass:
			case TileID.MushroomGrass:
				newTileType = TileID.Mud;
				break;
			case TileID.Mud:
				newTileType = TileID.Silt;
				break;
			}

			if( newTileType.HasValue ) {
				tile.type = newTileType.Value;
			} else {
				WorldGen.KillTile( tileX, tileY, false, false, true );
			}

			ParticleFxHelpers.MakeDustCloud( new Vector2((tileX*16) + 8, (tileY*16) + 8), 1, 0.3f, 0.65f );

			WorldGen.SquareTileFrame( tileX, tileY, true );
		}
	}
}
