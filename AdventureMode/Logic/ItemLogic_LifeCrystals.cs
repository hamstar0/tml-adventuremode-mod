using System;
using Terraria;


namespace AdventureMode.Logic {
	static partial class ItemLogic {
		public static void OnLifeCrystalConsume( Player player ) {
			if( AMConfig.Instance.ReducedLifeCrystalStatIncrease ) {
				player.statLifeMax -= 10;
				ItemLogic.ModifyLifeCrystalPopupText();
			}
		}


		////////////////

		private static void ModifyLifeCrystalPopupText() {
			for( int idx = 0; idx < Main.combatText.Length; idx++ ) {
				CombatText txt = Main.combatText[idx];
				if( txt == null || !txt.active ) { continue; }

				if( txt.text.Equals( "20" ) ) {
					txt.text = "10";	// hackish!
					break;
				}
			}
		}
	}
}
