using System;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static bool UpdateRodOfDiscordUse( AdventureModePlayer myplayer ) {
			if( !AdventureModeConfig.Instance.RodOfDiscordChaosStateBlocksBlink ) {
				return true;
			}

			int buffIdx = myplayer.player.FindBuffIndex( BuffID.ChaosState );
			bool isUsingItem = myplayer.player.itemAnimation >= 1;
			//bool firstUseOfItem = this.player.itemAnimation >= this.player.HeldItem.useAnimation - 1;

			if( buffIdx != -1 ) {
				if( !myplayer.IsChaosStateChecked ) {
					myplayer.IsChaosStateChecked = true;

					/*var reason = PlayerDeathReason.ByCustomReason( this.player.name + " splinched." );
					int dmg = this.player.statLifeMax2 / 7;
					dmg = (int)((float)dmg * AdventureModeConfig.Instance.RodOfDiscordPainIncreaseMultiplier);

					if( dmg > 0 ) {
						PlayerHelpers.RawHurt( this.player, reason, dmg, 0 );
					}*/

					myplayer.player.AddBuff( BuffID.ChaosState, AdventureModeConfig.Instance.AddedRodOfDiscordChaosStateTime );
				}
			} else {
				myplayer.IsChaosStateChecked = false;
			}

			return isUsingItem || !myplayer.IsChaosStateChecked;
		}
	}
}
