using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.ID;
using ModLibsCore.Services.Timers;


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

				if( ProjectileLogic.RopeCoils.ContainsKey( who ) ) {
					break;
				}

				//

				ProjectileLogic.RopeCoils[ who ] = type;

				Timers.RunUntil( () => {
					bool isCoil = Main.projectile[who]?.active == true
						&& ProjectileLogic.RopeCoils[who] == type;

					if( !isCoil ) {
						ProjectileLogic.RopeCoils.Remove( who );
					}

					return isCoil;
				}, false );

				//

				projectile.velocity *= 0.65f;

				break;
			}
		}
	}
}
