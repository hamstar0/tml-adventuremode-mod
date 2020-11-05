using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	class AMProjectile : GlobalProjectile {
		public override bool PreAI( Projectile projectile ) {
			if( projectile.aiStyle == 7 && !projectile.npcProj ) {
				ProjectileLogic.UpdateForGrapple( projectile );
			} else {
				switch( projectile.type ) {
				case ProjectileID.RopeCoil:
				case ProjectileID.SilkRopeCoil:
				case ProjectileID.VineRopeCoil:
				case ProjectileID.WebRopeCoil:
					ProjectileLogic.UpdateForRopeCoil( projectile );
					break;
				}
			}

			return base.PreAI( projectile );
		}

		public override void GrapplePullSpeed( Projectile projectile, Player player, ref float speed ) {
			ProjectileLogic.UpdateGrapplePullSpeed( projectile, ref speed );
		}
	}
}
