using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class DefoliationChargeRecipe : ModRecipe {
		public DefoliationChargeRecipe( DefoliationChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			this.AddIngredient( ModContent.ItemType<SeismicChargeItem>(), 1 );
			this.AddIngredient( ModContent.ItemType<GreenOrbItem>(), 1 );

			this.SetResult( myitem, 1 );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
