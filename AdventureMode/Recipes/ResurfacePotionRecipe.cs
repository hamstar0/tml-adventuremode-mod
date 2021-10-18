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
			this.AddIngredient( ItemID.FallenStar, 1 );
			this.AddIngredient( ItemID.Bottle, 3 );

			//if( AMConfig.Instance.EnableAlchemyRecipes ) {
			//	this.AddIngredient( ItemID.Bottle, 3 );
			//} else {
			//	this.AddIngredient( ItemID.LesserHealingPotion, 3 );
			//}

			this.SetResult( myitem, ResurfacePotionRecipe.GetStackAmount );

			//

			AMMod.Instance.AdditionalWhitelistedRecipesByItemType.Add( myitem.item.type );
		}

		public override bool RecipeAvailable() {
			return AMConfig.Instance.ResurfacePotionRecipeEnabled;
		}
	}
}
