using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;
using ModLibsGeneral.Libraries.World;
using AdventureMode.Recipes;


namespace AdventureMode.Items {
	public class ResurfacePotionItem : ModItem {
		public static void CheckItemForPlayer( Player player, Item item ) {
			if( player.itemAnimation <= 0 ) {
				return;
			}

			if( player.itemTime == 0 ) {
				player.itemTime = PlayerHooks.TotalUseTime( item.useTime, player, item );
			} else if( player.itemTime == 2 ) {
				ResurfacePotionItem.ApplyWarp( player, item );
			}
		}


		////////////////
		
		public static bool CanResurface( Player player, out string result ) {
			int tileX = (int)player.MountedCenter.X / 16;
			int tileY = (int)player.MountedCenter.Y / 16;
			Tile tile = Main.tile[tileX, tileY];

			// Cannot resurface if above surface
			if( tileY < WorldLocationLibraries.SurfaceLayerBottomTileY ) {
				//if( tile?.wall == 0 ) {
				result = "Too close to surface.";
				return false;
			}

			if( WorldLocationLibraries.IsLavaLayer(player.Center) || tileY >= WorldLocationLibraries.UnderworldLayerTopTileY ) {
				result = "Too deep.";
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

			for( int i = 0; i < 70; i++ ) {
				int idx = Dust.NewDust(
					player.position,
					player.width,
					player.height,
					15,
					player.velocity.X * 0.2f,
					player.velocity.Y * 0.2f,
					150,
					Color.Cyan,
					1.2f
				);
				Main.dust[idx].velocity *= 0.5f;
			}

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
			PlayerWarpLibraries.Teleport( player, myplayer.ResurfacePoint );
			player.immune = immune;
			player.immuneTime = immuneTime;

			for( int i = 0; i < 70; i++ ) {
				int idx = Dust.NewDust( player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.2f );
				Main.dust[idx].velocity *= 0.5f;
			}

			if( ItemLoader.ConsumeItem(item, player) && item.stack > 0 ) {
				item.stack--;
			}
		}



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Resurface Potion" );
			this.Tooltip.SetDefault(
				"Warps you to your last sight of daylight/moonlight"
				+"\nDoes not work if too deep"
			);
		}
		
		public override void SetDefaults() {
			this.item.width = 14;
			this.item.height = 24;

			this.item.maxStack = 30;

			this.item.value = Item.buyPrice( silver: 5 );
			this.item.rare = ItemRarityID.Blue;

			this.item.consumable = true;
			this.item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
			
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useAnimation = 17;
			this.item.useTime = 17;
			this.item.useTurn = true;

			this.item.UseSound = SoundID.Item3;
		}

		public override void AddRecipes() {
			var recipe = new ResurfacePotionRecipe( this );
			recipe.AddRecipe();
		}


		////////////////

		public override bool ConsumeItem( Player player ) {
			if( !ResurfacePotionItem.CanResurface(player, out string msg) ) {
				Main.NewText( msg, Color.Yellow );
				return false;
			}
			return true;
		}
	}
}
