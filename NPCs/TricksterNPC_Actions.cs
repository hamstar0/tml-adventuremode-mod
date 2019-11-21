using AdventureMode.Buffs;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.FX;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace TheTrickster.NPCs {
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

		public void Dodge( int dodgeRadius ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			int minDist = 18 * 16;
			int minDistSqr = minDist * minDist;

			int plrWho = this.npc.HasPlayerTarget
					? this.npc.target
					: this.npc.FindClosestPlayer();
			Player player = Main.player[ plrWho ];

			Vector2 dir, testPos, groundPos;
			bool isOnGround;
			int tileX=0, tileY=0;
			do {
				dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();
				dir *= minDist + ( rand.NextFloat() * (dodgeRadius - minDist) );

				testPos = player.Center + dir;
				dodgeRadius += 1;

				isOnGround = WorldHelpers.DropToGround( testPos, false, TilePattern.CommonSolid, out groundPos );
				if( !isOnGround ) { continue; }

				if( Vector2.DistanceSquared(player.Center, groundPos) < minDistSqr ) {
					isOnGround = false;
					continue;
				}

				tileX = (int)groundPos.X / 16;
				tileY = (int)groundPos.Y / 16;
			} while( !isOnGround || !TilePattern.NonSolid.CheckArea( new Rectangle(tileX-1, tileY-3, 3, 3) ) );

			// Before
			ParticleFxHelpers.MakeTeleportFx( this.npc.position, 16, this.npc.width, this.npc.height );

			this.npc.position = groundPos - new Vector2( 0, this.npc.height + 1 );
			this.npc.netUpdate = true;

			// After
			ParticleFxHelpers.MakeTeleportFx( this.npc.position, 16, this.npc.width, this.npc.height );
		}


		////////////////

		public void Flee() {

		}


		////////////////

		public void Defeat() {

		}
	}
}
