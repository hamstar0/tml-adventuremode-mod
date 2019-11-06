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


namespace AdventureMode {
	class AdventureModePlayer : ModPlayer {
		private bool IsAlertedToBossesWhileDead = false;


		////////////////

		public float NecrotisPercent { get; internal set; }

		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void ResetEffects() {
			this.NecrotisPercent = 1f;
		}

		////

		public override void PreUpdate() {
			NecrotisDebuff.UpdateForPlayer( this.player );
		}

		public override void PreUpdateBuffs() {
			int dangBuffIds = this.player.FindBuffIndex( BuffID.Dangersense );

			if( dangBuffIds != -1 ) {
				if( this.player.buffTime[dangBuffIds] > 60 * 60 * 2 ) {
					this.player.buffTime[dangBuffIds] = 60 * 60 * 2;
				}
			}
		}

		public override void PreUpdateMovement() {
			if( this.NecrotisPercent < 1f ) {
				NecrotisDebuff.ApplyEffect( this.player, this.NecrotisPercent );
			}
		}

		////

		public override void UpdateDead() {
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
				var binocs = new Item();
				binocs.SetDefaults( ItemID.Binoculars );
				binocs.stack = 1;
				var houseKits = new Item();
				houseKits.SetDefaults( ModContent.ItemType<HouseFurnishingKitItem>() );
				houseKits.stack = 3;

				items.Add( torches );
				items.Add( ropes );
				items.Add( binocs );
				items.Add( houseKits );
			}
		}
	}
}
