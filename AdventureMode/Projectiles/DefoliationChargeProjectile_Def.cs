using System;
using Terraria;


namespace AdventureMode.Projectiles {
	public partial class DefoliationChargeProjectile : BaseExplosiveChargeProjectile {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Defoliation Charge" );

			this.drawOriginOffsetX = 0;
			this.drawOriginOffsetY = -8;
		}
	}
}
