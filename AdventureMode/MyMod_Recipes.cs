using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public override void AddRecipes() {
			RecipeLogic.AddRoDRecipes();
			RecipeLogic.RemoveVanillaBasicHookRecipes();
		}

		public override void PostAddRecipes() {
			RecipeLogic.RemoveTileRequirements();
			RecipeLogic.TweakBowlRecipe();
		}
	}
}
