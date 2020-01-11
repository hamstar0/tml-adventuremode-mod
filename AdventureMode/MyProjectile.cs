using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Tiles;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		public static bool? IsTileGrappleable( Tile tile ) {
			if( !tile.active() || !Main.tileSolid[tile.type] ) {
				return null;
			}

			switch( tile.type ) {
			case TileID.Platforms:
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
				return AdventureModeConfig.Instance.GrappleOnlyWoodAndPlatforms
					? (bool?)false
					: null;
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
			
			int baseX = (int)( ((projectile.velocity.X * 1.01f) + projectile.Center.X) / 16f );
			int baseY = (int)( ((projectile.velocity.Y * 1.01f) + projectile.Center.Y) / 16f );

			for( int x=baseX-1; x<baseX+1; x++ ) {
				for( int y=baseY-1; y<baseY+1; y++ ) {
					bool? isGrappleable = AdventureModeProjectile.IsTileGrappleable( Main.tile[x, y] );

					if( isGrappleable.HasValue ) {
						if( !isGrappleable.Value ) {
							projectile.ai[0] = 1;
						}
						break;
					}
				}
			}

		}
	}
}
