using HamstarHelpers.Helpers.Debug;
using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModePlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			if( Main.netMode != 2 ) {
				this.PreUpdateLocal();
			}
		}
		
		private void PreUpdateLocal() {
			if( Main.myPlayer != this.player.whoAmI ) {
				return;
			}
		}
	}
}
