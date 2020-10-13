using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AdventureModePlayer : ModPlayer {
		public override void PreUpdate() {
		}

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
