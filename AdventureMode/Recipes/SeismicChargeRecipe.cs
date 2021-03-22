using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class SeismicChargeRecipe : ModRecipe {
		public SeismicChargeRecipe( SeismicChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			this.AddIngredient( ItemID.Bomb, 16 );
			this.AddIngredient( ItemID.Obsidian, 1 );
			this.AddRecipeGroup( "AdventureMode.Orb", 1 );

			this.SetResult( myitem, 8 );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
