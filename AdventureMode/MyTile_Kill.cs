using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	class AdventureModeExtendedTileHooks : ILoadable {
		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnModsUnload() {
		}

		void ILoadable.OnPostModsLoad() {
			TileLogic.InitializeTileKillBehaviors();
		}
	}




	//partial class AMTile : GlobalTile {
	//}
}