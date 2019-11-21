using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using Nihilism;
using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AdventureModeMod Instance { get; private set; }

		public static AdventureModeConfig Config => ModContent.GetInstance<AdventureModeConfig>();



		////////////////

		private (ushort TileType, int Width, int Height)[] FurnitureCycle;
		private int FurnitureCycleIdx = 0;



		////////////////

		public AdventureModeMod() {
			AdventureModeMod.Instance = this;
		}

		////////////////

		public override void Load() {
			EntityGroups.Enable();
			NihilismAPI.InstancedFiltersOn();
			NihilismAPI.OnSyncOrWorldLoad( (isSync) => {
				if( isSync ) { return; }
				this.ApplyNihilismFilters();
			}, 0f );

			this.LoadChestImplants();
			this.LoadHouseFurnishingKit();
		}

		////

		public override void Unload() {
			AdventureModeMod.Instance = null;
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
