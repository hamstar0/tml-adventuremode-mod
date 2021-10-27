using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items;
using AdventureMode.Logic;
using AdventureMode.Recipes;


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
			
			AddRG( "Sand",
				ItemID.SandBlock,
				ItemID.CrimsandBlock,
				ItemID.EbonsandBlock,
				ItemID.PearlsandBlock
			);
			
			AddRG( "Gems",
				ItemID.Amethyst,
				ItemID.Sapphire,
				ItemID.Topaz,
				ItemID.Emerald,
				ItemID.Ruby,
				ItemID.Diamond,
				ItemID.Amber
			);
		}

		////

		public override void AddRecipes() {
			var newRoDRecipe1 = new RodOfDiscordRecipe( false );
			newRoDRecipe1.AddRecipe();

			var newRoDRecipe2 = new RodOfDiscordRecipe( true );
			newRoDRecipe2.AddRecipe();
			
			var bottleRecipe = new BottleRecipe();
			bottleRecipe.AddRecipe();
			
			var wandSparkRecipe = new WandOfSparkingRecipe();
			wandSparkRecipe.AddRecipe();

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
