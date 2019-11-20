using AdventureMode.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.NPCs {
	partial class TricksterNPC : ModNPC {
		public void LaunchAttack() {
			this.CreateLaunchedAttackFX( TricksterNPC.AttackRadius );

			int radiusSqr = TricksterNPC.AttackRadius * TricksterNPC.AttackRadius;
			int invulnBuffType = ModContent.BuffType<DegreelessnessBuff>();

			for( int i=0; i<Main.npc.Length; i++ ) {
				NPC otherNpc = Main.npc[i];
				if( otherNpc == null || !otherNpc.active || otherNpc.friendly || otherNpc.immortal || otherNpc.whoAmI == this.npc.whoAmI ) {
					continue;
				}

				if( Vector2.DistanceSquared(otherNpc.Center, this.npc.Center) < radiusSqr ) {
					otherNpc.AddBuff( invulnBuffType, TricksterNPC.InvulnTickDuration );
				}
			}
		}


		////////////////

		public void Evade() {

		}


		////////////////

		public void Flee() {

		}
	}
}
