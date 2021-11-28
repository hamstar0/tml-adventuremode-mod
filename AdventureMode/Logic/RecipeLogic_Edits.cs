using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsEntityGroups.Services.EntityGroups;
using Ergophobia.Items.FramingPlank;
using SoulBarriers.Items;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		public static void EditExistingRecipes( ISet<int> additionalWhitelistedItemTypes ) {
			RecipeLogic.EditBowlRecipe();
			RecipeLogic.EditFramingPlankRecipe();
			RecipeLogic.EditBossRecipes();
			RecipeLogic.EditPBGRecipeIf();

			ModContent.GetInstance<EntityGroups>().OnLoad += () => {
				ISet<int> whitelistTypes = RecipeLogic.GetWhitelistedRecipes( additionalWhitelistedItemTypes );

				RecipeLogic.ApplyRecipeWhitelisting( whitelistTypes );
				RecipeLogic.ApplyNewTileRequirements( whitelistTypes );
			};
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

		////

		public static void EditFramingPlankRecipe() {
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<FramingPlankItem>() );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				re.DeleteTile( TileID.Sawmill );
			}
		}

		////
		
		public static void EditPBGRecipeIf() {
			if( ModLoader.GetMod("RuinedItems") != null ) {
				RecipeLogic.EditPBGRecipe_WeakRefs_RuinedItems();
			}
		}

		private static void EditPBGRecipe_WeakRefs_RuinedItems() {
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<PBGItem>() );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				re.DeleteIngredient( ItemID.Nanites );
				re.AddIngredient( ModContent.ItemType<RuinedItems.Items.MagitechScrapItem>(), 2 );
			}
		}

		////

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
//LogLibraries.Log( "boss item "+bossItemType+" recipes: "+rf.SearchRecipes().Count );

				foreach( Recipe r in rf.SearchRecipes() ) {
					RecipeLogic.EditBossRecipe( r );
				}
			}
		}

		private static void EditBossRecipe( Recipe r ) {
			var re = new RecipeEditor( r );
			re.AcceptRecipeGroup( "AdventureMode.StrangePlant" );

			int grpId = RecipeGroup.recipeGroupIDs[ "AdventureMode.StrangePlant" ];
			if( !RecipeGroup.recipeGroups.ContainsKey(grpId) ) {
				LogLibraries.Warn( "Could not find recipe group #: "+grpId+" (AdventureMode.StrangePlant)" );

				return;
			}

			RecipeGroup rg = RecipeGroup.recipeGroups[ grpId ];
			if( rg.IconicItemIndex <= -1 ) {
				LogLibraries.Warn( "Could not find recipe group's (AdventureMode.StrangePlant) 'iconic item'" );

				return;
			}

			int grpItemType = rg.ValidItems[ rg.IconicItemIndex ];
//LogLibraries.Log( "grpId:"+grpId+ " - rg:"+string.Join(",", rg.ValidItems)+" - grpItemType:"+grpItemType );
			re.AddIngredient( grpItemType, AMConfig.Instance.StrangePlantsAddedPerBossSummonItemRecipe );
		}
	}
}
