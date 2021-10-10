using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class ThermalChargeRecipe : ModRecipe {
		public ThermalChargeRecipe( ThermalChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			this.AddIngredient( ModContent.ItemType<SeismicChargeItem>(), SeismicChargeRecipe.GetStackAmount );
			this.AddIngredient( ModContent.ItemType<RedOrbItem>(), 1 );

			this.SetResult( myitem, SeismicChargeRecipe.GetStackAmount );

			//

			AMMod.Instance.AdditionalWhitelistedRecipesByItemType.Add( myitem.item.type );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
