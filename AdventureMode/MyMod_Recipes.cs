using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Recipes;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public override void AddRecipes() {
			var newRoDRecipe1 = new RodOfDiscordRecipe( false );
			newRoDRecipe1.AddRecipe();

			var newRoDRecipe2 = new RodOfDiscordRecipe( true );
			newRoDRecipe2.AddRecipe();
		}

		public override void PostAddRecipes() {
			if( AdventureModeConfig.Instance.RemoveRecipeTileRequirements ) {
				for( int i = 0; i < Main.recipe.Length; i++ ) {
					Recipe recipe = Main.recipe[i];
					if( recipe == null ) { continue; }

					for( int j = 0; j < recipe.requiredTile.Length; j++ ) {
						//if( recipe.requiredTile[j] != TileID.Furnaces ) {
						recipe.requiredTile[j] = -1;
					}
				}
			}

			for( int i = 0; i < Main.recipe.Length; i++ ) {
				Recipe recipe = Main.recipe[i];
				if( recipe?.createItem?.type != ItemID.Bowl ) { continue; }

				foreach( Item item in recipe.requiredItem ) {
					if( item?.type == ItemID.ClayBlock ) { continue; }

					item.type = ItemID.Wood;
					break;
				}
				break;
			}
		}
	}
}
