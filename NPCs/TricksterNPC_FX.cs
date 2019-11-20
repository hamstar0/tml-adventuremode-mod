using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Draw;
using HamstarHelpers.Helpers.FX;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureMode.NPCs {
	partial class TricksterNPC : ModNPC {
		public static void AnimateAttackBurstFX( Vector2 position, float radius, int particles ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();

			for( int i = 0; i < particles; i++ ) {
				Vector2 dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();
				Vector2 dustPos = position + (dir * rand.NextFloat() * radius);

				int dustIdx = Dust.NewDust(
					Position: dustPos,
					Width: 1,
					Height: 1,
					Type: 269,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.White,
					Scale: 1f
				);
				Dust dust = Main.dust[dustIdx];
				dust.noGravity = true;
			}
		}

		public static void AnimateAttackChargeAreaFX( Vector2 position, float percent, int particles ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float radius = 80f;
			bool willDrawLightning = ( rand.NextFloat() * 3f ) < ( percent * percent );

			for( int i = 0; i < particles; i++ ) {
				if( rand.NextFloat() >= percent ) { continue; }

				var dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();

				float offset = 8f + ( rand.NextFloat() * ( radius - 8f ) );
				offset *= 0.75f + percent;

				Vector2 dustPos = position + (dir * offset);

				Vector2 pullVelocity = -dir * ( offset / 8f );

				Dust dust = Dust.NewDustPerfect(
					Position: dustPos,
					Type: 222,//	133:0.5, 222:0.5, 246
					Velocity: pullVelocity,
					Scale: 0.5f
				);
				dust.noGravity = true;
				dust.fadeIn = 0.4f;//0.33f;

				///

				if( willDrawLightning ) {
					willDrawLightning = false;

					int duration = 3;
					DrawHelpers.AddPostDrawTilesAction( () => {
						float scaleBase = rand.NextFloat();
						float scale = 0.01f + ( scaleBase * 0.1f );
						Color color = Color.White * ( 0.5f + ( scaleBase * 0.5f ) );

						LightningFxHelpers.DrawLightning( position, position + (dir * offset * 3f), scale, color );
						return duration-- > 0;
					} );
				}
			}
		}



		////////////////

		private SoundEffectInstance AttackChargeSoundInstance;



		////////////////

		private void RunFX() {
			if( this.State == TricksterStates.Attack ) {
				if( this.AttackChargeSoundInstance == null || this.AttackChargeSoundInstance.State != SoundState.Playing ) {
					this.AttackChargeSoundInstance = Main.PlaySound( SoundID.Item93, this.npc.Center );
				}

				float percent = this.ElapsedStateTicks / (float)this.GetCurrentStateTickDuration();

				TricksterNPC.AnimateAttackChargeAreaFX( this.npc.Center, percent, 12 );
			} else {
				if( this.AttackChargeSoundInstance != null && this.AttackChargeSoundInstance.State == SoundState.Playing ) {
					this.AttackChargeSoundInstance.Stop();
					this.AttackChargeSoundInstance = null;
				}
			}
		}


		////

		public void CreateLaunchedAttackFX( int radius ) {
			Vector2 pos = this.npc.Center;

			Main.PlaySound( SoundID.Item113, pos );

			Task.Run( () => TricksterNPC.AnimateAttackBurstFX( pos, radius, radius / 3 ) );
			Task.Run( () => TricksterNPC.AnimateAttackBurstFX( pos, radius, radius / 3 ) );
			Task.Run( () => TricksterNPC.AnimateAttackBurstFX( pos, radius, radius / 3 ) );
			Task.Run( () => {
				TricksterNPC.AnimateAttackBurstFX( pos, 64, 64 );
				TricksterNPC.AnimateAttackBurstFX( pos, 32, 32 );
			} );
		}
	}
}
