using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Items.Attributes;
using AdventureMode.Recipes;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		public static IEnumerable<ModRecipe> CreateItemRefundRecipes() {
			ISet<int> oreItemTypes = RecipeLogic.GetOreItemTypes();
			ISet<int> oreMadeItemTypes = RecipeLogic.GetOreMadeItemRecipes( oreItemTypes );
			ISet<Recipe> oreArmorRecipes = RecipeLogic.GetOreArmorRecipesFromOreMadeItems( oreMadeItemTypes );

			return RecipeLogic.CreateOreArmorRefundRecipes( oreItemTypes, oreArmorRecipes );
		}


		////
		
		private static IEnumerable<ModRecipe> CreateOreArmorRefundRecipes(
					ISet<int> oreItemTypes,
					ISet<Recipe> oreArmorRecipes ) {
			Item PickSignificantIngredientItem( Item ing1, Item ing2 ) {
				if( !ing1.active || !oreItemTypes.Contains(ing1.type) ) {
					return ing2;
				}
				if( !ing2.active || !oreItemTypes.Contains(ing2.type) ) {
					return ing1;
				}
				return ing1.stack > ing2.stack
					? ing1 : ing2;
			}

			//

			var refundRecipes = new List<ModRecipe>();

			foreach( Recipe oreArmorRecipe in oreArmorRecipes ) {
				Item mostUsedIngredient = oreArmorRecipe.requiredItem
					.Aggregate( PickSignificantIngredientItem );

				// Not an ore
				if( !oreItemTypes.Contains(mostUsedIngredient.type) ) {
					continue;
				}

				ModRecipe refundRecipe = OreRefundRecipe.CreateRecipe(
					oreArmorRecipe.createItem.Clone(),
					mostUsedIngredient.Clone()
				);
				refundRecipes.Add( refundRecipe );
			}

			return refundRecipes;
		}


		////////////////

		private static ISet<int> GetOreItemTypes() {
			var oreItemTypes = new HashSet<int>();

			// Get item ids of ores
			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.createItem.createTile <= -1 ) {
					continue;
				}
				if( TileID.Sets.Ore[recipe.createItem.createTile] ) {
					oreItemTypes.Add( recipe.createItem.type );
				}
			}

			return oreItemTypes;
		}

		private static ISet<int> GetOreMadeItemRecipes( ISet<int> oreItemTypes ) {
			var oreMadeItemTypes = new HashSet<int>();    // bars, tiles, mythril anvil, adamantite forge, etc.
			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.requiredItem.Any( i => i.active && oreItemTypes.Contains(i.type) ) ) {
					oreMadeItemTypes.Add( recipe.createItem.type );
				}
			}

			return oreMadeItemTypes;
		}

		private static ISet<Recipe> GetOreArmorRecipesFromOreMadeItems( ISet<int> oreMadeItemTypes ) {
			var oreArmorRecipes = new HashSet<Recipe>();

			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.requiredItem.Any( i => i.active && oreMadeItemTypes.Contains(i.type) ) ) {
					if( ItemAttributeLibraries.IsArmor(recipe.createItem) ) {
						oreArmorRecipes.Add( recipe );
					}
				}
			}

			return oreArmorRecipes;
		}
	}
}
