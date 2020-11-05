using System;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;


namespace AdventureMode.Logic {
	static partial class PlayerLogic {
		public static void UpdateDeadDuringBoss( AMPlayer myplayer ) {
			if( !AMConfig.Instance.RespawnBlockedDuringBosses ) {
				return;
			}

			if( !Main.npc.Any( n => n?.active == true && n.boss && n.netID != NPCID.WallofFlesh && n.netID != NPCID.WallofFleshEye ) ) {
				myplayer.IsAlertedToBossesWhileDead = false;
				return;
			}

			if( !myplayer.IsAlertedToBossesWhileDead ) {
				myplayer.IsAlertedToBossesWhileDead = true;
				Main.NewText( "Respawning is blocked while bosses are active.", Color.Red );
			}

			if( myplayer.player.respawnTimer < 60 ) {
				myplayer.player.respawnTimer = 59;
			}
		}
	}
}
