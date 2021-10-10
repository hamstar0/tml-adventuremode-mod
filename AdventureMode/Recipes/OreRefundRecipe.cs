using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Recipes {
	class OreRefundRecipe : ModRecipe {
		public static OreRefundRecipe CreateRecipe( Item originalRecipeResultItem, Item mostUsedIngredient ) {
			return new OreRefundRecipe(
				tileType: TileID.Furnaces,
				result: (mostUsedIngredient.type, mostUsedIngredient.stack),
				ingredientItemTypes: new int[] { originalRecipeResultItem.type }
			);
		}



		////////////////

		public OreRefundRecipe( int tileType, (int itemType, int stack) result, params int[] ingredientItemTypes )
					: base( AMMod.Instance ) {
			this.AddTile( tileType );

			foreach( int ingredientItemType in ingredientItemTypes ) {
				this.AddIngredient( ingredientItemType, 1 );
			}

			this.SetResult( result.itemType, result.stack );

			//

			//AMMod.Instance.AdditionalWhitelistedRecipesByItemType.Add( result.itemType );
		}


		public override bool RecipeAvailable() {
			return AMConfig.Instance.OreRefundRecipesEnabled;
		}
	}
}
