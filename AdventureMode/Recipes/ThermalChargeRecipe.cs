using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class ThermalChargeRecipe : ModRecipe {
		public ThermalChargeRecipe( ThermalChargeItem myitem ) : base( AMMod.Instance ) {
			this.AddTile( TileID.WorkBenches );

			int amt = SeismicChargeRecipe.GetStackAmount / 2;

			this.AddIngredient( ModContent.ItemType<SeismicChargeItem>(), amt );
			//this.AddIngredient( ModContent.ItemType<RedOrbItem>(), 1 );
			this.AddIngredient( ItemID.MeteoriteBar, 1 );
			this.AddIngredient( ItemID.Gel, 15 );

			this.SetResult( myitem, amt );

			//

			AMMod.Instance.AdditionalWhitelistedRecipesByItemType.Add( myitem.item.type );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.SeismicChargeRecipeEnabled;
		}
	}
}
