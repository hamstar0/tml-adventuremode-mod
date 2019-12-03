using AdventureMode.Mods;
using AdventureMode.Recipes;
using HamstarHelpers.Helpers.Debug;
using System;
using Terraria;
using Terraria.ModLoader;


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
			this.ModInteractions.LoadNihilism();
			this.ModInteractions.LoadChestImplants();
			this.ModInteractions.LoadHouseFurnishingKitAndMountedMagicMirrors();
			this.ModInteractions.LoadTricksterAndLockedAbilies();
			this.ModInteractions.LoadLockedAbilities();
			this.ModInteractions.LoadStaffOfGaia();
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
