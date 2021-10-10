using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using HUDElementsLib.Elements.Samples;
using AdventureMode.Mods;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremode-mod";


		////////////////

		public static AMMod Instance { get; private set; }



		////////////////

		internal TimerHUD RaftTimerHUD;


		////////////////

		public AdventureModeModInteractions ModInteractions { get; } = new AdventureModeModInteractions();

		////

		internal ISet<int> AdditionalWhitelistedRecipesByItemType { get; } = new HashSet<int>();



		////////////////

		public AMMod() {
			AMMod.Instance = this;
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
//DebugLibraries.Print( "use item debuf", "ia:"+plr.itemAnimation+", it:"+plr.itemTime+", cc:"+plr.CCed+", int:"+plr.mouseInterface );
			WorldLogic.UpdateWorldSpawnForInvasionStateIf();
		}
	}
}
