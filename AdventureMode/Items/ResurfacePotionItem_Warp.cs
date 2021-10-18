using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;
using ModLibsGeneral.Libraries.World;


namespace AdventureMode.Items {
	public partial class ResurfacePotionItem : ModItem {
		public static bool CanResurface( Player player, out string result ) {
			int tileY = (int)player.MountedCenter.Y / 16;

			// Cannot resurface if above surface
			if( tileY < WorldLocationLibraries.SurfaceLayerBottomTileY ) {
				//if( tile?.wall == 0 ) {
				result = "Too close to surface.";
				return false;
			}
			
//Main.NewText( "Is lava? "+WorldLocationLibraries.IsLavaLayer(tileY, out float lp)+" ("+tileY+", "+lp+") - "+Main.rockLayer );
			if( WorldLocationLibraries.IsLavaLayer(tileY, out float lavaPerc) && lavaPerc >= 0.5f ) {
				result = "Too deep.";
				return false;
			}

			var myplayer = player.GetModPlayer<AMPlayer>();
			if( myplayer.ResurfacePoint == default ) {
				result = "No recent surface point has been encountered.";
				return false;
			}

			result = "Resurfacing allowed.";
			return true;
		}


		////////////////

		public static Vector2? GetResurfacePointIf( Player player ) {
			int tileX = (int)player.Center.X / 16;
			int tileY = (int)player.Center.Y / 16;

			// Cannot resurface below surface
			if( tileY > WorldLocationLibraries.SurfaceLayerBottomTileY ) {
				return null;
			}

			// Cannot resurface without open air
			Tile tile = Main.tile[tileX, tileY];
			if( tile?.active() == true || tile?.wall != 0 ) {
				return null;
			}

			return player.position;
		}


		////////////////

		public static void ApplyWarp( Player player, Item item ) {
			var myplayer = player.GetModPlayer<AMPlayer>();

			ResurfacePotionItem.CreateWarpParticles( player.position, player.width, player.height );

			player.grappling[0] = -1;
			player.grapCount = 0;

			int projLen = Main.projectile.Length;
			for( int i = 0; i < projLen; i++ ) {
				if( Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 7 ) {
					Main.projectile[i].Kill();
				}
			}

			bool immune = player.immune;
			int immuneTime = player.immuneTime;
			PlayerWarpLibraries.Teleport( player, myplayer.ResurfacePoint, 0 );
			player.immune = immune;
			player.immuneTime = immuneTime;

			ResurfacePotionItem.CreateWarpParticles( player.position, player.width, player.height );

			if( ItemLoader.ConsumeItem(item, player) && item.stack > 0 ) {
				item.stack--;
			}
		}



		////////////////

		public static void CreateWarpParticles( Vector2 position, int width, int height ) {
			for( int i = 0; i < 70; i++ ) {
				int idx = Dust.NewDust(
					position,
					width,
					height,
					15,
					0f,
					0f,
					150,
					Color.Cyan,
					1.2f
				);
				Main.dust[idx].velocity *= 0.5f;
			}
		}
	}
}
