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
			ISet<int> oreMadeItemTypes = RecipeLogic.GetOreMadeItemRecipes();
			ISet<Recipe> oreArmorRecipes = RecipeLogic.GetOreArmorRecipesFromOreMadeItems( oreMadeItemTypes );

			return RecipeLogic.CreateOreArmorRefundRecipes( oreArmorRecipes );
		}


		////
		
		private static IEnumerable<ModRecipe> CreateOreArmorRefundRecipes( ISet<Recipe> oreArmorRecipes ) {
			var refundRecipes = new List<ModRecipe>();

			foreach( Recipe oreArmorRecipe in oreArmorRecipes ) {
				Item mostUsedIngredient = oreArmorRecipe.requiredItem.Aggregate( (l, r) => {
					if( l.IsAir ) {
						return r;
					}
					if( r.IsAir ) {
						return l;
					}
					return l.stack > r.stack
						? l : r;
				} );

				ModRecipe refundRecipe = OreRefundRecipe.CreateRecipe( oreArmorRecipe.createItem, mostUsedIngredient );
				refundRecipes.Add( refundRecipe );
			}

			return refundRecipes;
		}


		////////////////

		private static ISet<int> GetOreMadeItemRecipes() {
			var oreItemTypes = new HashSet<int>();
			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.createItem.createTile <= -1 ) {
					continue;
				}
				if( TileID.Sets.Ore[recipe.createItem.createTile] ) {
					oreItemTypes.Add( recipe.createItem.type );
				}
			}

			var oreMadeItemTypes = new HashSet<int>();    // bars, tiles, mythril anvil, adamantite forge, etc.
			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.requiredItem.Any( i => !i.IsAir && oreItemTypes.Contains( i.type ) ) ) {
					oreMadeItemTypes.Add( recipe.createItem.type );
				}
			}

			return oreMadeItemTypes;
		}

		private static ISet<Recipe> GetOreArmorRecipesFromOreMadeItems( ISet<int> oreMadeItemTypes ) {
			var oreArmorRecipes = new HashSet<Recipe>();

			foreach( Recipe recipe in Main.recipe ) {
				if( recipe.requiredItem.Any( i => !i.IsAir && oreMadeItemTypes.Contains( i.type ) ) ) {
					if( ItemAttributeLibraries.IsArmor( recipe.createItem ) ) {
						oreArmorRecipes.Add( recipe );
					}
				}
			}

			return oreArmorRecipes;
		}
	}
}
