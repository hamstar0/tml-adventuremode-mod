using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.Hooks.LoadHooks;
using AdventureMode.Recipes;
using Ergophobia.Items.FramingPlank;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		public static void AddNewRecipes() {
			var newRoDRecipe1 = new RodOfDiscordRecipe( false );
			newRoDRecipe1.AddRecipe();

			var newRoDRecipe2 = new RodOfDiscordRecipe( true );
			newRoDRecipe2.AddRecipe();

			foreach( ModRecipe refundRecipe in RecipeLogic.CreateItemRefundRecipes() ) {
				refundRecipe.AddRecipe();
			}
		}


		public static void EditExistingRecipes() {
			RecipeLogic.EditBowlRecipe();
			RecipeLogic.EditFramingPlankRecipe();

			CustomLoadHooks.AddHook( EntityGroups.LoadedAllValidator, ( _ ) => {
				RecipeLogic.ApplyRecipeWhitelistingAndNewTileRequirements();
				return false;
			} );
		}

		////

		//public static void RemoveCanopicJarRecipe() {	<- See Mods/Necrotis.cs
		//}


		////////////////

		public static void EditBowlRecipe() {
			var rf = new RecipeFinder();
			rf.SetResult( ItemID.Bowl );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				if( re.DeleteIngredient(ItemID.ClayBlock) ) {
					re.AddIngredient( ItemID.Wood, 5 );
				}
			}
		}

		public static void EditFramingPlankRecipe() {
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<FramingPlankItem>() );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				re.DeleteTile( TileID.Sawmill );
			}
		}

		public static void EditBossRecipes() {
			if( AMConfig.Instance.StrangePlantsAddedPerBossSummonItemRecipe <= 0 ) {
				return;
			}

			int[] bossItemTypes = new int[] {
				ItemID.SlimeCrown,
				ItemID.SuspiciousLookingEye,
				ItemID.WormFood,
				ItemID.BloodySpine,
				ItemID.Abeemination,
				ItemID.MechanicalWorm,
				ItemID.MechanicalEye,
				ItemID.MechanicalSkull
			};

			foreach( int bossItemType in bossItemTypes ) {
				var rf = new RecipeFinder();
				rf.SetResult( bossItemType );

				foreach( Recipe r in rf.SearchRecipes() ) {
					RecipeLogic.EditBossRecipe( r );
				}
			}
		}

		////

		private static void EditBossRecipe( Recipe r ) {
			var re = new RecipeEditor( r );
			re.AcceptRecipeGroup( "AdventureMode.StrangePlant" );

			int grpId = RecipeGroup.recipeGroupIDs[ "AdventureMode.StrangePlant" ];
			RecipeGroup rg = RecipeGroup.recipeGroups[ grpId ];

			int grpItemType = rg.ValidItems[ rg.IconicItemIndex ];
			re.AddIngredient( grpItemType, AMConfig.Instance.StrangePlantsAddedPerBossSummonItemRecipe );
		}


		////////////////

		public static void ApplyRecipeWhitelistingAndNewTileRequirements() {
			bool overrideTile = AMConfig.Instance.OverrideRecipeTileRequirements;
			ISet<int> whitelistTypes = RecipeLogic.GetWhitelistedRecipes();

			RecipeLogic.ApplyRecipeWhitelistingAndNewTileRequirements( overrideTile, whitelistTypes );
		}
	}
}
