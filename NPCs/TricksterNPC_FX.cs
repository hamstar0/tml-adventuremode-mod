using HamstarHelpers.Helpers.Debug;
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

			this.AnimateAttackChargeCore( percent );
			this.AnimateAttackChargeCore( percent );
			//this.AnimateAttackChargeArea( percent );
		}

		////

		private void AnimateAttackChargeCore( float percent ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			if( rand.NextFloat() < percent ) { return; }

			float radius = percent * 16f;
			Vector2 position = this.npc.Center;
			position.X -= radius;
			position.Y -= radius;

			int dustIdx = Dust.NewDust(
				Position: position,
				Width: (int)(radius * 2f),
				Height: (int)(radius * 2f),
				Type: 269,
				SpeedX: 0f,
				SpeedY: 0f,
				Alpha: 0,
				newColor: Color.White,
				Scale: 2f * percent
			);
			Dust dust = Main.dust[dustIdx];
			dust.noGravity = true;
		}
	}
}
