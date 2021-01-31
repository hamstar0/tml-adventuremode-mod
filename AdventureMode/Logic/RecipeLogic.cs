using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AdventureMode.Recipes;
using Ergophobia.Items;


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
			var rf = new RecipeFinder();
			rf.SetResult( ItemID.Bowl );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				if( re.DeleteIngredient(ItemID.ClayBlock) ) {
					re.AddIngredient( ItemID.Wood, 5 );
				}
			}
		}

		public static void TweakFramingPlankRecipe() {
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<FramingPlankItem>() );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				re.DeleteTile( TileID.Sawmill );
			}
		}
	}
}
