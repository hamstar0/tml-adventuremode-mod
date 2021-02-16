using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	class AMProjectile : GlobalProjectile {
		public override bool PreAI( Projectile projectile ) {
			switch( projectile.type ) {
			case ProjectileID.RopeCoil:
			case ProjectileID.SilkRopeCoil:
			case ProjectileID.VineRopeCoil:
			case ProjectileID.WebRopeCoil:
				ProjectileLogic.UpdateForRopeCoil( projectile );
				break;
			}

			return base.PreAI( projectile );
		}
	}
}
