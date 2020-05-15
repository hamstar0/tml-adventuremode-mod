using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Mods;
using AdventureMode.Recipes;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }



		////////////////

		public AdventureModeModInteractions ModInteractions { get; } = new AdventureModeModInteractions();



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			this.ModInteractions.LoadBullwhip();
			this.ModInteractions.LoadNihilism();
			this.ModInteractions.LoadChestImplants();
			this.ModInteractions.LoadPrefabsKitAndMountedMagicMirrors();
			this.ModInteractions.LoadTricksterAndLockedAbilies();
			this.ModInteractions.LoadLockedAbilities();
			if( ModLoader.GetMod("StaffOfGaia") != null ) {
				this.ModInteractions.LoadStaffOfGaia();
			}
			this.ModInteractions.LoadOrbs();
		}

		////

		public override void Unload() {
			AdventureModeMod.Instance = null;
		}


		////////////////

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


		////////////////

		/*public override void PostSetupContent() {
			CustomHotkeys.BindActionToKey1( "Illuminate", () => {
				var manaTileSingleton = ModContent.GetInstance<ManaCrystalShardTile>();
				foreach( (int tileX, IDictionary<int, float> tileYs) in manaTileSingleton.IlluminatedCrystals.ToArray() ) {
					foreach( (int tileY, float illum) in tileYs.ToArray() ) {
						manaTileSingleton.IlluminatedCrystals[tileX][tileY] = 1f;
					}
				}
				Main.NewText("Lit!");
			} );
		}*/
	}
}
