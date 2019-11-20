using AdventureMode.NPCs;
using HamstarHelpers.Services.Timers;
using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Buffs {
	class DegreelessnessBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Degreelessness Mode" );
			this.Description.SetDefault( "Power overwhelming!" );
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			this.ApplyFx( player );

			player.immune = true;

			Timers.SetTimer( "AdventureModeDGLN_P_" + player.whoAmI, 2, false, () => {
				player.immune = false;
				return true;
			} );
		}


		public override void Update( NPC npc, ref int buffIndex ) {
			this.ApplyFx( npc );

			//npc.immortal = true;
			npc.dontTakeDamage = true;

			Timers.SetTimer( "AdventureModeDGLN_N_" + npc.whoAmI, 2, false, () => {
				//npc.immortal = false;
				npc.dontTakeDamage = false;
				return true;
			} );
		}

		////////////////

		private void ApplyFx( Entity entity ) {
			int radius = ( entity.width + entity.height ) / 2;
			TricksterNPC.AnimateAttackBurstFX( entity.Center, radius, radius / 10 );
		}
	}
}
