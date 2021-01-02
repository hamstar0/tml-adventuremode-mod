using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void AddRecipes() {
			RecipeLogic.AddRoDRecipes();
		}

		public override void PostAddRecipes() {
			RecipeLogic.ApplyRecipeWhitelistingAndNewTileRequirements();
			RecipeLogic.TweakBowlRecipe();
		}
	}
}
