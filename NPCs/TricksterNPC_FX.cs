using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Draw;
using HamstarHelpers.Helpers.FX;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureMode.NPCs {
	partial class TricksterNPC : ModNPC {
		private void AnimateAttackCharge() {
			float percent = this.ElapsedStateTicks / (float)this.GetCurrentStateTickDuration();

			//this.AnimateAttackChargeCore( percent );
			this.AnimateAttackChargeArea( percent );
		}

		////

		/*private void AnimateAttackChargeCore( float percent ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();

			int particles = 4;
			for( int i = 0; i < particles; i++ ) {
				if( rand.NextFloat() >= percent ) { continue; }

				float radius = 1f;
				radius += (float)i * (2f + (8f * percent));

				Vector2 position = this.npc.Center;
				position.X -= radius;
				position.Y -= radius;

				float scale = particles - i;
				scale *= 0.25f + (0.75f * percent);

				int dustIdx = Dust.NewDust(
					Position: position,
					Width: (int)( radius * 2f ),
					Height: (int)( radius * 2f ),
					Type: 269,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.White,
					Scale: scale
				);
				Dust dust = Main.dust[dustIdx];
				dust.noGravity = true;
			}
		}*/

		private void AnimateAttackChargeArea( float percent ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float radius = 80f;
			bool willDrawLightning = (rand.NextFloat() * 3f) < (percent * percent);

			int particles = 16;
			for( int i=0; i<particles; i++ ) {
				if( rand.NextFloat() >= percent ) { continue; }

				var dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();

				float offset = 8f + (rand.NextFloat() * (radius - 8f));
				offset *= 0.75f + percent;

				Vector2 position = this.npc.Center;
				position += dir * offset;

				Vector2 pullVelocity = -dir * (offset / 8f);

				Dust dust = Dust.NewDustPerfect(
					Position: position,
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
						float scale = 0.01f + (scaleBase * 0.1f);
						Color color = Color.White * (0.5f + (scaleBase * 0.5f));

						LightningFxHelpers.DrawLightning( this.npc.Center, this.npc.Center + (dir * offset * 3f), scale, color );
						return duration-- > 0;
					} );
				}
			}
		}
	}
}
