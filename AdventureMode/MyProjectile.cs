using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Tiles;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		public static bool IsTileGrappleable( Tile tile ) {
			if( !tile.active() || (!Main.tileSolid[tile.type] && tile.type != TileID.MinecartTrack) ) {
				return false;
			}

			switch( tile.type ) {
			case TileID.Platforms:
			case TileID.MinecartTrack:
			case TileID.WoodBlock:
			case TileID.BorealWood:
			case TileID.DynastyWood:
			case TileID.LivingWood:
			case TileID.LeafBlock:
			case TileID.PalmWood:
			case TileID.SpookyWood:
			case TileID.LivingMahogany:
			case TileID.RichMahogany:
			case TileID.LivingMahoganyLeaves:
				return true;
			default:
				if( tile.type == ModContent.TileType<FramingPlankTile>() ) {
					return true;
				}
				return !AdventureModeConfig.Instance.GrappleOnlyWoodAndPlatforms;
			}
		}



		////////////////

		public override bool PreAI( Projectile projectile ) {
			if( projectile.aiStyle == 7 && !projectile.npcProj ) {
				if( AdventureModeConfig.Instance.GrappleOnlyWoodAndPlatforms ) {
					this.AdjustGrappleAttachState( projectile );
				}
			}
			return base.PreAI( projectile );
		}

		public override void GrapplePullSpeed( Projectile projectile, Player player, ref float speed ) {
			int x = (int)( projectile.Center.X / 16f );
			int y = (int)( projectile.Center.Y / 16f );

			bool? isGrappleable = AdventureModeProjectile.IsTileGrappleable( Main.tile[x, y] );

			if( isGrappleable.HasValue && !isGrappleable.Value ) {
				speed = 0f;
			}
		}

		////

		private void AdjustGrappleAttachState( Projectile projectile ) {
			if( projectile.ai[0] != 0 && projectile.ai[0] != 2 ) {
				return;
			}

			Vector2 projCen = projectile.Center;
			Vector2 projVel = projectile.velocity;
			int nowX = (int)(projCen.X / 16f);
			int nowY = (int)(projCen.Y / 16f);
			int nextX = (int)( ((projVel.X * 0.5f) + projCen.X) / 16f );
			int nextY = (int)( ((projVel.Y * 0.5f) + projCen.Y) / 16f );
			int lastX = (int)( (projVel.X + projCen.X) / 16f );
			int lastY = (int)( (projVel.Y + projCen.Y) / 16f );
			
/*int bah = 120;
Timers.SetTimer( "grap", 3, false, () => {
	Dust.QuickDust( new Point(x, y), isNextSolid ? Color.Red : Color.Green );
	return bah-- > 0;
} );*/
			if( projectile.ai[0] == 0 ) {
				Tile nextTile = Main.tile[nextX, nextY];
				Tile lastTile = Main.tile[lastX, lastY];

				if( nextTile?.active() == true && (Main.tileSolid[nextTile.type] || nextTile.type == TileID.MinecartTrack) ) {
					if( !AdventureModeProjectile.IsTileGrappleable(nextTile) ) {
						projectile.ai[0] = 1;
					}
				} else
				if( lastTile?.active() == true && (Main.tileSolid[lastTile.type] || lastTile.type == TileID.MinecartTrack) ) {
					if( !AdventureModeProjectile.IsTileGrappleable( lastTile ) ) {
						projectile.ai[0] = 1;
					}
				}
			} else {
				Tile nowTile = Main.tile[nowX, nowY];
				
				if( nowTile?.active() == true && (Main.tileSolid[nowTile.type] || nowTile.type == TileID.MinecartTrack) ) {
					if( !AdventureModeProjectile.IsTileGrappleable(nowTile) ) {
						projectile.ai[0] = 1;
					}
				}
			}
		}
	}
}
