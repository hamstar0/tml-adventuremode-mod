using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using AdventureMode.Recipes;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		public static void AddRoDRecipes() {
			var newRoDRecipe1 = new RodOfDiscordRecipe( false );
			newRoDRecipe1.AddRecipe();

			var newRoDRecipe2 = new RodOfDiscordRecipe( true );
			newRoDRecipe2.AddRecipe();
		}

		////

		//public static void RemoveCanopicJarRecipe() {	<- See Mods/Necrotis.cs
		//}


		////////////////

		public static void ApplyRecipeWhitelistingAndNewTileRequirements() {
			bool overrideTile = AMConfig.Instance.OverrideRecipeTileRequirements;
			ISet<int> whitelistTypes = RecipeLogic.GetWhitelistedRecipes();

			RecipeLogic.ApplyRecipeWhitelistingAndNewTileRequirements( overrideTile, whitelistTypes );
		}


		////////////////

		public static void TweakBowlRecipe() {
			for( int i = 0; i < Main.recipe.Length; i++ ) {
				Recipe recipe = Main.recipe[i];
				if( recipe?.createItem?.type != ItemID.Bowl ) { continue; }

				foreach( Item item in recipe.requiredItem ) {
					if( item?.type == ItemID.ClayBlock ) { continue; }

					item.type = ItemID.Wood;
					break;
				}

				break;
			}
		}
	}
}
