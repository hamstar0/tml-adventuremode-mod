using AdventureMode.Buffs;
using HamstarHelpers.Helpers.Debug;
using HouseFurnishingKit.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace AdventureMode {
	partial class AdventureModePlayer : ModPlayer {
		internal ISet<string> IntroducedNpcUniqueKeys { get; } = new HashSet<string>();

		////

		private bool IsAlertedToBossesWhileDead = false;
		private bool IsChaosStateChecked = false;


		////////////////

		public float NecrotisAmount { get; internal set; } = 0f;

		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "introduced_npc_count" ) ) {
				return;
			}

			int count = tag.GetInt( "introduced_npc_count" );

			for( int i = 0; i < count; i++ ) {
				string uniqueKey = tag.GetString( "introduced_npc_" + i );
				this.IntroducedNpcUniqueKeys.Add( uniqueKey );
			}
		}
		
		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "introduced_npc_count", this.IntroducedNpcUniqueKeys.Count }
			};

			int i = 0;
			foreach( string key in this.IntroducedNpcUniqueKeys ) {
				tag["introduced_npc_" + i] = key;
			}

			return tag;
		}


		////////////////

		public override void PreUpdate() {
			NecrotisDebuff.UpdateBuffDurationForPlayer( this.player );
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

		public override void PreUpdateMovement() {
			if( this.player.HasBuff(ModContent.BuffType<NecrotisDebuff>()) ) {
				NecrotisDebuff.ApplyEffect( this.player );
			}
		}

		////
		
		public override void UpdateDead() {
			if( !AdventureModeConfig.Instance.RespawnBlockedDuringBosses ) {
				return;
			}

			if( !Main.npc.Any( n => n.active && n.boss ) ) {
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

		public override void SetupStartInventory( IList<Item> items, bool mediumcoreDeath ) {
			if( !mediumcoreDeath ) {
				var torches = new Item();
				torches.SetDefaults( ItemID.Torch );
				torches.stack = 10;

				var ropes = new Item();
				ropes.SetDefaults( ItemID.Rope );
				ropes.stack = 200;

				items.Add( torches );
				items.Add( ropes );
			}
		}


		////////////////
		
		public override bool PreItemCheck() {
			Item heldItem = this.player.HeldItem;
			bool enabled = true;

			if( heldItem?.type == ItemID.RodofDiscord ) {
				if( AdventureModeConfig.Instance.RodOfDiscordChaosStateBlocksBlink ) {
					enabled = this.CheckRodOfDiscord();
				}
			}

			return enabled;
		}

		private bool CheckRodOfDiscord() {
			if( this.player.itemAnimation == 0 ) {
				if( this.IsChaosStateChecked ) {
					return false;
				}
			}
			
			if( this.player.itemAnimation >= this.player.HeldItem.useAnimation - 1 ) {
				if( this.player.HasBuff(BuffID.ChaosState) ) {
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
				}
			}
				
			if( this.IsChaosStateChecked && !this.player.HasBuff(BuffID.ChaosState) ) {
				this.IsChaosStateChecked = false;
			}

			return true;
		}
	}
}
