using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using Nihilism;
using System;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }

		public static AdventureModeConfig Config => ModContent.GetInstance<AdventureModeConfig>();



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			EntityGroups.Enable();
			NihilismAPI.InstancedFiltersOn();

			NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync ) { return; }
				//NihilismAPI.ClearFiltersForCurrentWorld( true );
				//NihilismAPI.SetRecipeBlacklistGroupEntry( "Any Item", true );
				//NihilismAPI.SetItemBlacklistGroupEntry( "Any Placeable", true );
				NihilismAPI.NihilateCurrentWorld( true );
			}, 0f );
		}

		public override void Unload() {
			AdventureModeMod.Instance = null;
		}


		////////////////

		public override void PostSetupContent() {
			/*CustomHotkeys.BindActionToKey1( "Illuminate", () => {
				var manaTileSingleton = ModContent.GetInstance<ManaCrystalShardTile>();
				foreach( (int tileX, IDictionary<int, float> tileYs) in manaTileSingleton.IlluminatedCrystals.ToArray() ) {
					foreach( (int tileY, float illum) in tileYs.ToArray() ) {
						manaTileSingleton.IlluminatedCrystals[tileX][tileY] = 1f;
					}
				}
				Main.NewText("Lit!");
			} );*/
		}
	}
}
