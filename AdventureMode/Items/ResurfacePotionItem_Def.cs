using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;
using ModLibsGeneral.Libraries.World;
using AdventureMode.Recipes;


namespace AdventureMode.Items {
	public partial class ResurfacePotionItem : ModItem {
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
