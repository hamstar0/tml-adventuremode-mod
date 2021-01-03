using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Hooks.LoadHooks;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void AddRecipes() {
			RecipeLogic.AddRoDRecipes();
		}

		public override void PostAddRecipes() {
			RecipeLogic.TweakBowlRecipe();

			LoadHooks.AddPostModLoadHook( () => {
				RecipeLogic.ApplyRecipeWhitelistingAndNewTileRequirements();
			} );
		}
	}
}
