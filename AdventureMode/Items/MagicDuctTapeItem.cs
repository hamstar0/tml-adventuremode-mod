using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Recipes;


namespace AdventureMode.Items {
	class MagicDuctTapeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Seismic Charge" );
			this.Tooltip.SetDefault( "Tossed as a bomb, exploding after a few seconds"
				+"\nExplosion produces focused tremors that break down certain materials"
				+"\nBreaks down sandstone into sand, mud into silt, ice into slush"
				+"\nAlso breaks certain brick types"
			);
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 20;
			this.item.maxStack = 99;
			this.item.material = true;
			this.item.value = Item.buyPrice( 0, 0, 10, 0 );
			this.item.rare = ItemRarityID.Blue;
		}
		
		public override void AddRecipes() {
			var recipe = new SeismicChargeRecipe( this );
			recipe.AddRecipe();
		}
	}
}
