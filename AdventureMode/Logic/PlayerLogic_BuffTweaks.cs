using System;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static void UpdateBuffs( Player player ) {
			int dangBuffIds = player.FindBuffIndex( BuffID.Dangersense );

			if( dangBuffIds != -1 ) {
				int maxDuration = AMConfig.Instance.MaximumDangersenseBuffDuration;

				if( player.buffTime[dangBuffIds] > maxDuration ) {
					player.buffTime[dangBuffIds] = maxDuration;
				}
			}
		}
	}
}
