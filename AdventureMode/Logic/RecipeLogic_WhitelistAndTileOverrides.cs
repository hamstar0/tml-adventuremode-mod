using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		private static void ApplyRecipeWhitelistingAndNewTileRequirements( bool overrideTile, ISet<int> whitelistTypes ) {
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

				if( !overrideTile ) {
					continue;
				}

				var re = new RecipeEditor( recipe );
				bool usesTile = false;
				bool usesDemonAltar = false;

				foreach( int reqTileId in recipe.requiredTile ) {
					if( reqTileId < 0 ) { continue; }

					usesTile = true;
					usesDemonAltar = reqTileId == TileID.DemonAltar;

					if( !usesDemonAltar ) {
						re.DeleteTile( reqTileId );
					}
				}

				if( usesTile ) {
					if( !usesDemonAltar ) {
						re.AddTile( TileID.WorkBenches );
					}
				}
			}

			foreach( Recipe recipe in toDelete ) {
				RecipeEditor re = new RecipeEditor( recipe );
				re.DeleteRecipe();
			}
		}
	}
}
