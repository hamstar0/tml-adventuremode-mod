using AdventureMode.Recipes;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using Nihilism;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }



		////////////////

		private (ushort TileType, int Width, int Height)[] FurnitureCycle;
		private int FurnitureCycleIdx = 0;



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			this.LoadNihilism();
			this.LoadChestImplants();
			this.LoadHouseFurnishingKitAndMountedMagicMirrors();
			this.LoadTricksterAndLockedAbilies();
			this.LoadLockedAbilities();
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
