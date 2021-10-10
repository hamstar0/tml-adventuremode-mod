using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;
using AdventureMode.Recipes;
using Orbs.Items;


namespace AdventureMode {
	partial class AMMod : Mod {
		public override void AddRecipeGroups() {
			void AddRG( string name, params int[] itemTypes ) {
				string displayName = "Any " + name;
				string grpName = "AdventureMode." + name.Replace( " ", "" );

				var rg = new RecipeGroup( () => displayName, itemTypes );

				int grpIdx = RecipeGroup.RegisterGroup( grpName, rg );
//this.Logger.Info( "dname: "+displayName+", gname: "+grpName+", grp: "+rg.IconicItemIndex+", idx: "+grpIdx+", items: "+string.Join(",", itemTypes) );
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
			var newRoDRecipe1 = new RodOfDiscordRecipe( false );
			newRoDRecipe1.AddRecipe();

			var newRoDRecipe2 = new RodOfDiscordRecipe( true );
			newRoDRecipe2.AddRecipe();

			foreach( ModRecipe refundRecipe in RecipeLogic.CreateItemRefundRecipes() ) {
				refundRecipe.AddRecipe();
			}
		}

		public override void PostAddRecipes() {
			RecipeLogic.EditExistingRecipes( this.AdditionalWhitelistedRecipesByItemType );

			this.AdditionalWhitelistedRecipesByItemType.Clear();
		}
	}
}
