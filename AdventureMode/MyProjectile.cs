using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		public override bool PreAI( Projectile projectile ) {
			if( projectile.aiStyle == 7 && !projectile.npcProj ) {
				ProjectileLogic.UpdateForGrapple( projectile );
			}
			return base.PreAI( projectile );
		}

		public override void GrapplePullSpeed( Projectile projectile, Player player, ref float speed ) {
			ProjectileLogic.UpdateGrapplePullSpeed( projectile, ref speed );
		}
	}
}
