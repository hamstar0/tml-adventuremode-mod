using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Items;


namespace AdventureMode.Recipes {
	class ResurfacePotionRecipe : ModRecipe {
		public static int GetStackAmount => 3;



		////////////////

		public ResurfacePotionRecipe( ResurfacePotionItem myitem ) : base( AMMod.Instance ) {
			//this.AddTile( TileID.WorkBenches );
			
			this.AddIngredient( ItemID.RecallPotion, 1 );

			this.SetResult( myitem, ResurfacePotionRecipe.GetStackAmount );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.ResurfacePotionRecipeEnabled;
		}
	}
}
