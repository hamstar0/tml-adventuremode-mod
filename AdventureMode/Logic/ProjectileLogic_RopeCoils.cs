using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.Timers;


namespace AdventureMode.Logic {
	static partial class ProjectileLogic {
		private static IDictionary<int, int> RopeCoils = new ConcurrentDictionary<int, int>();



		////////////////

		public static void UpdateForRopeCoil( Projectile projectile ) {
			switch( projectile.type ) {
			//case ProjectileID.SilkRopeCoil:
			case ProjectileID.VineRopeCoil:
			case ProjectileID.WebRopeCoil:
			case ProjectileID.RopeCoil:
				int who = projectile.whoAmI;
				int type = projectile.type;

				if( !ProjectileLogic.RopeCoils.ContainsKey( who ) ) {
					ProjectileLogic.RopeCoils[ who ] = type;

					Timers.RunUntil( () => {
						bool isActive = Main.projectile[who]?.active == true && ProjectileLogic.RopeCoils[who] == type;
						if( !isActive ) {
							ProjectileLogic.RopeCoils.Remove( who );
						}

						return isActive;
					}, false );

					projectile.velocity *= 0.65f;
				}
				break;
			}
		}
	}
}
