using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using AdventureMode.Mods;
using AdventureMode.Logic;


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
			void loadMod( string modName, Action loader ) {
				try {
					loader();
				} catch( Exception e ) {
					LogHelpers.Warn( "Error loading "+modName+" - "+e.ToString() );
				}
			}

			//

			EntityGroups.Enable();

			loadMod( "GreenHell", this.ModInteractions.LoadGreenHell );
			loadMod( "Bullwhip", this.ModInteractions.LoadBullwhip );
			loadMod( "Nihilism", this.ModInteractions.LoadNihilism );
			loadMod( "Necrotis", this.ModInteractions.LoadNecrotis );
			loadMod( "CursedBrambles", this.ModInteractions.LoadCursedBrambles );
			loadMod( "ChestImplants", this.ModInteractions.LoadChestImplants );
			loadMod( "ErgophobiaAndMMM", this.ModInteractions.LoadErgophobiaAndMountedMagicMirrors );
			loadMod( "TricksterAndLockedAbilies", this.ModInteractions.LoadTricksterAndLockedAbilies );
			loadMod( "LockedAbilities", this.ModInteractions.LoadLockedAbilities );
			loadMod( "Orbs", this.ModInteractions.LoadOrbs );
			loadMod( "TerrainRemixer", this.ModInteractions.LoadTerrainRemixer );
			loadMod( "LoreAndObjectives", this.ModInteractions.LoadLoreAndObjectives );
			loadMod( "TheMadRanger", this.ModInteractions.LoadTheMadRanger );
			loadMod( "PowerfulMagic", this.ModInteractions.LoadPowerfulMagic );
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


		////////////////

		public override void PostUpdateEverything() {
			WorldLogic.UpdateWorldSpawnForInvasionState();
		}
	}
}
