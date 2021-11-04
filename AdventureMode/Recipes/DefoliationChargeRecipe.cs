using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class DefoliationChargeRecipe : ModRecipe {
		public DefoliationChargeRecipe( DefoliationChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			int amt = SeismicChargeRecipe.GetStackAmount / 2;

			this.AddIngredient( ModContent.ItemType<SeismicChargeItem>(), amt );
			//this.AddIngredient( ModContent.ItemType<GreenOrbItem>(), 1 );
			this.AddIngredient( ItemID.JungleSpores, 1 );
			this.AddRecipeGroup( "AdventureMode.EvilSeeds", 1 );

			this.SetResult( myitem, amt );

			//
			
			AMMod.Instance.AdditionalWhitelistedRecipesByItemType.Add( myitem.item.type );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
