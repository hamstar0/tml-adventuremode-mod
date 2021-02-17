using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;
using Orbs.Items;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void AddRecipeGroups() {
			void AddRG( string name, params int[] itemTypes ) {
				string displayName = "Any " + name;
				string grpName = "AdventureMode." + name.Replace( " ", "" );

				var rg = new RecipeGroup( () => displayName, itemTypes );

				RecipeGroup.RegisterGroup( grpName, rg );
			}

			//

			AddRG( "Strange Plant",
				ItemID.StrangePlant1,
				ItemID.StrangePlant2,
				ItemID.StrangePlant3,
				ItemID.StrangePlant4
			);
			AddRG( "Orb",
				ModContent.ItemType<BlueOrbItem>(),
				ModContent.ItemType<CyanOrbItem>(),
				ModContent.ItemType<GreenOrbItem>(),
				ModContent.ItemType<PinkOrbItem>(),
				ModContent.ItemType<PurpleOrbItem>(),
				ModContent.ItemType<RedOrbItem>(),
				ModContent.ItemType<BrownOrbItem>(),
				ModContent.ItemType<YellowOrbItem>(),
				ModContent.ItemType<WhiteOrbItem>()
			);
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
