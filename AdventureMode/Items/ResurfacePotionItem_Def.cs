using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Recipes;


namespace AdventureMode.Items {
	public partial class ResurfacePotionItem : ModItem {
		public static void PreItemCheck_RunBehaviorIf( Player player, Item item ) {
			if( item.type != ModContent.ItemType<ResurfacePotionItem>() ) {
				return;
			}

			// Item animation I guess means it's activated?
			if( player.itemAnimation >= 1 ) {
				// Item animation without `itemTime` means it's newly activated, and assumes itemAnimation expires first?
				if( player.itemTime == 0 ) {
					player.itemTime = PlayerHooks.TotalUseTime( item.useTime, player, item );
				} else if( player.itemTime == 2 ) {
					if( ResurfacePotionItem.CanResurface(player, out string msg) ) {
						ResurfacePotionItem.ApplyWarp( player, item );
					} else {
						Main.NewText( msg, Color.Yellow );
					}
				}
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
			//this.item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
			
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

		public override bool UseItem( Player player ) {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			return ResurfacePotionItem.CanResurface( player, out _ );
		}
	}
}
