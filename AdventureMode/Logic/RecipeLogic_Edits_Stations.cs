using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Items;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		private static void ApplyNewTileRequirements( ISet<int> recipeItemTypes ) {
			for( int i = 0; i < Main.recipe.Length; i++ ) {
				Recipe recipe = Main.recipe[i];
				int itemType = recipe.createItem.type;
				if( itemType == 0 ) {
					continue;
				}

				if( !recipeItemTypes.Contains( itemType ) ) {
					continue;
				}

				var re = new RecipeEditor( recipe );
				bool usesTile = false;
				bool usesDemonAltar = false;
				bool usesTinkerersWorkbench = false;

				foreach( int reqTileId in recipe.requiredTile ) {
					if( reqTileId < 0 ) { continue; }

					usesTile = true;
					usesDemonAltar = reqTileId == TileID.DemonAltar;
					usesTinkerersWorkbench = reqTileId == TileID.TinkerersWorkbench;

					// Clear existing crafting stations (unless station = Demon Altar)
					if( !usesDemonAltar ) {
						re.DeleteTile( reqTileId );
					}
				}

				if( usesTile ) {
					// All non-Demon Altar items simply use workbench only
					if( !usesDemonAltar ) {
						re.AddTile( TileID.WorkBenches );
					}

					// Tinkerer's Workbench items require added ingredient
					if( usesTinkerersWorkbench ) {
						re.AddIngredient( ModContent.ItemType<MagicDuctTapeItem>(), 1 );
					}
				}
			}
		}
	}
}
