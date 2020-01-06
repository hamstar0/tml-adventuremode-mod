using AdventureMode.Tiles;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Items {
	public class FramingPlankItem : ModItem {
		public override void SetStaticDefaults() {
			string text = "May be used for light patching or small structures";
			if( !AdventureModeConfig.Instance.RemoveRecipeTileRequirements ) {
				text += "\n" + "Crafted with a sawmill station";
			}

			this.Tooltip.SetDefault( text );
			this.DisplayName.SetDefault( "Framing Plank" );
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 999;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = 1;
			this.item.consumable = true;
			this.item.createTile = ModContent.TileType<FramingPlankTile>();
		}

		////////////////

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddRecipeGroup( "Wood", 4 );
			recipe.AddTile( TileID.Sawmill );
			recipe.SetResult( this, 1 );
			recipe.AddRecipe();
		}
	}
}
