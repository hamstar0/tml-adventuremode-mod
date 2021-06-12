using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsEntityGroups.Services.EntityGroups;
using Ergophobia.Items.FramingPlank;
using Necrotis.Items;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		public static void EditExistingRecipes() {
			RecipeLogic.EditBowlRecipe();
			RecipeLogic.EditFramingPlankRecipe();
			RecipeLogic.EditElixirRecipe();
			RecipeLogic.EditBossRecipes();

			ModContent.GetInstance<EntityGroups>().OnLoad += () => {
				ISet<int> whitelistTypes = RecipeLogic.GetWhitelistedRecipes();

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
		
		public static void EditElixirRecipe() {
			var rf = new RecipeFinder();
			rf.SetResult( ModContent.ItemType<ElixirOfLifeItem>() );

			foreach( Recipe r in rf.SearchRecipes() ) {
				var re = new RecipeEditor( r );
				re.DeleteIngredient( ItemID.ShinePotion );
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
			RecipeGroup rg = RecipeGroup.recipeGroups[ grpId ];

			int grpItemType = rg.ValidItems[ rg.IconicItemIndex ];
//LogLibraries.Log( "grpId:"+grpId+ " - rg:"+string.Join(",", rg.ValidItems)+" - grpItemType:"+grpItemType );
			re.AddIngredient( grpItemType, AMConfig.Instance.StrangePlantsAddedPerBossSummonItemRecipe );
		}
	}
}
