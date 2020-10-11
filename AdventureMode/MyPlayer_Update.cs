using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode {
	partial class AdventureModePlayer : ModPlayer {
		public override void PreUpdate() {
		}

		public override void PreUpdateBuffs() {
			int dangBuffIds = this.player.FindBuffIndex( BuffID.Dangersense );

			if( dangBuffIds != -1 ) {
				int maxDuration = AdventureModeConfig.Instance.MaximumDangersenseBuffDuration;

				if( this.player.buffTime[dangBuffIds] > maxDuration ) {
					this.player.buffTime[dangBuffIds] = maxDuration;
				}
			}
		}


		////////////////

		public override void UpdateDead() {
			if( !AdventureModeConfig.Instance.RespawnBlockedDuringBosses ) {
				return;
			}

			if( !Main.npc.Any(n => n?.active == true && n.boss && n.netID != NPCID.WallofFlesh && n.netID != NPCID.WallofFleshEye) ) {
				this.IsAlertedToBossesWhileDead = false;
				return;
			}

			if( !this.IsAlertedToBossesWhileDead ) {
				this.IsAlertedToBossesWhileDead = true;
				Main.NewText( "Respawning is blocked while bosses are active.", Color.Red );
			}

			if( this.player.respawnTimer < 60 ) {
				this.player.respawnTimer = 59;
			}
		}


		////////////////
		
		public override bool PreItemCheck() {
			Item heldItem = this.player.HeldItem;
			bool enabled = true;

			if( heldItem?.IsAir != true && heldItem.type == ItemID.RodofDiscord ) {
				if( AdventureModeConfig.Instance.RodOfDiscordChaosStateBlocksBlink ) {
					enabled = this.CheckRodOfDiscord();
				}
			}

			return enabled;
		}


		////////////////

		private bool CheckRodOfDiscord() {
			int buffIdx = this.player.FindBuffIndex( BuffID.ChaosState );
			bool isUsingItem = this.player.itemAnimation >= 1;
			//bool firstUseOfItem = this.player.itemAnimation >= this.player.HeldItem.useAnimation - 1;

			if( buffIdx != -1 ) {
				if( !this.IsChaosStateChecked ) {
					this.IsChaosStateChecked = true;

					/*var reason = PlayerDeathReason.ByCustomReason( this.player.name + " splinched." );
					int dmg = this.player.statLifeMax2 / 7;
					dmg = (int)((float)dmg * AdventureModeConfig.Instance.RodOfDiscordPainIncreaseMultiplier);

					if( dmg > 0 ) {
						PlayerHelpers.RawHurt( this.player, reason, dmg, 0 );
					}*/

					this.player.AddBuff( BuffID.ChaosState, AdventureModeConfig.Instance.AddedRodOfDiscordChaosStateTime );
				}
			} else {
				this.IsChaosStateChecked = false;
			}

			return isUsingItem || !this.IsChaosStateChecked;
		}
	}
}
