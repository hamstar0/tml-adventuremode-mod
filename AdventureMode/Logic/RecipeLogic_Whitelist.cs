using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		private static void ApplyRecipeWhitelisting( ISet<int> whitelistTypes ) {
			var toDelete = new List<Recipe>();

			for( int i = 0; i < Main.recipe.Length; i++ ) {
				Recipe recipe = Main.recipe[i];
				int itemType = recipe.createItem.type;
				if( itemType == 0 ) {
					continue;
				}

				if( !whitelistTypes.Contains( itemType ) ) {
					toDelete.Add( recipe );

					continue;
				}
			}

			foreach( Recipe recipe in toDelete ) {
				RecipeEditor re = new RecipeEditor( recipe );
				re.DeleteRecipe();
			}
		}
	}
}
