using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMPlayer : ModPlayer {
		//public override void PreUpdate() {
//int guide = NPC.FindFirstNPC( NPCID.Guide );
//if( guide != -1 ) {
//	DebugLibraries.Print( "guide", Main.npc[guide].position.ToString() );
//}
		//}

		public override void PreUpdateBuffs() {
			PlayerLogic.UpdateBuffs( this.player );
		}


		////////////////

		public override void UpdateDead() {
			PlayerLogic.UpdateDeadDuringBoss( this );
		}


		////////////////
		
		public override bool PreItemCheck() {
			Item heldItem = this.player.HeldItem;
			if( heldItem?.IsAir != true && heldItem.type == ItemID.RodofDiscord ) {
				return PlayerLogic.UpdateRodOfDiscordUse( this );
			}

			return true;
		}
	}
}
