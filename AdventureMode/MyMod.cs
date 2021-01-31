using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Mods;
using HamstarHelpers.Services.EntityGroups;


namespace AdventureMode {
	partial class AMMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AMMod Instance { get; private set; }



		////////////////

		public AdventureModeModInteractions ModInteractions { get; } = new AdventureModeModInteractions();



		////////////////

		public AMMod() {
			AMMod.Instance = this;
		}

		////////////////

		public override void Load() {
			EntityGroups.Enable();

			this.ModInteractions.LoadBullwhip();
			this.ModInteractions.LoadNihilism();
			this.ModInteractions.LoadNecrotis();
			this.ModInteractions.LoadCursedBrambles();
			this.ModInteractions.LoadChestImplants();
			this.ModInteractions.LoadErgophobiaAndMountedMagicMirrors();
			this.ModInteractions.LoadTricksterAndLockedAbilies();
			this.ModInteractions.LoadLockedAbilities();
			this.ModInteractions.LoadOrbs();
			this.ModInteractions.LoadTerrainRemixer();
			this.ModInteractions.LoadLoreAndObjectives();
			this.ModInteractions.LoadTheMadRanger();
			this.ModInteractions.LoadPowerfulMagic();
		}

		////

		public override void Unload() {
			AMMod.Instance = null;
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
