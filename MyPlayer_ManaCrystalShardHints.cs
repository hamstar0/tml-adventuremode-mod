﻿using FindableManaCrystals.Tiles;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModePlayer : ModPlayer {
		public float MeasureClosestManaCrystalShardTileDistance() {
			float closest = -1, current;
			int maxX = (int)(Main.screenPosition.X + Main.screenWidth);
			int maxY = (int)(Main.screenPosition.Y + Main.screenHeight);
			maxX = maxX >> 4;
			maxY = maxY >> 4;

			for( int i=(int)Main.screenPosition.X >> 4; i<maxX; i++ ) {
				for( int j=(int)Main.screenPosition.Y >> 4; j<maxY; j++ ) {
					Tile tile = Main.tile[i, j];

					if( tile == null || !tile.active() || tile.type != ModContent.TileType<ManaCrystalShardTile>() ) {
						continue;
					}

					current = Vector2.DistanceSquared(
						new Vector2( i, j ),
						new Vector2( maxX, maxY )
					);
					if( closest == -1 || current < closest ) {
						closest = current;
					}
				}
			}

			return closest == -1
				? -1
				: (float)Math.Sqrt( closest );
		}


		public void AnimateManaCrystalShardHint() {
			float manaProximity = this.MeasureClosestManaCrystalShardTileDistance();
			if( manaProximity <= 0 ) {
				return;
			}

			Timers.SetTimer( "ManaCrystalShardHint", (int)Math.Max(10, manaProximity), false, () => {
				int dustIdx = Dust.NewDust(
					Main.screenPosition,
					Main.screenWidth,
					Main.screenHeight,
					59,
					0f,
					0f,
					0,
					new Color( 255, 255, 255 ),
					1.25f
				);
				Dust dust = Main.dust[dustIdx];
				dust.noGravity = true;
				dust.noLight = true;

				float newManaProximity = this.MeasureClosestManaCrystalShardTileDistance();
				if( newManaProximity <= 0 ) {
					return 0;
				}

				return (int)Math.Max( 10, newManaProximity );
			} );
		}
	}
}
