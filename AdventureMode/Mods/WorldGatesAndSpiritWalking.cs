using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadWorldGatesAndSpiritWalking() {
			/*SpiritWalking.SpiritWalkingAPI.AddSpiritBallVelocityCalculationHook(
				this.DetectWorldGateSpiritBallCollision
			);*/
		}


		////////////////

		private bool DetectWorldGateSpiritBallCollision(
					Projectile proj,
					float lerpPerc,
					ref Vector2 intendedVel,
					ref float currSpeedScale ) {
			IEnumerable<WorldGates.GateBarrier> gates = WorldGates.WorldGatesAPI.GetGateBarriers();

			Vector2 toBePos = SpiritWalking.SpiritWalkingAPI.PredictSpiritBallPosition(
				currentVelocity: proj.velocity,
				intendedVelocity: intendedVel,
				currentFlightSpeedScale: currSpeedScale,
				lerpPercent: lerpPerc
			);

			var projArea = new Rectangle(
				x: (int)toBePos.X,
				y: (int)toBePos.Y,
				width: proj.width,
				height: proj.height
			);

			return gates.All( b => !b.WorldArea.Intersects(projArea) );
		}
	}
}
