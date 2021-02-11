using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void AddRecipeGroups() {
			var rg = new RecipeGroup( () => "AdventureMode.StrangePlants",
				ItemID.StrangePlant1,
				ItemID.StrangePlant2,
				ItemID.StrangePlant3,
				ItemID.StrangePlant4
			);

			RecipeGroup.RegisterGroup( "Strange Plants", rg );
		}

		////

		public override void AddRecipes() {
			RecipeLogic.AddNewRecipes();
		}

		public override void PostAddRecipes() {
			RecipeLogic.EditExistingRecipes();
		}
	}
}
