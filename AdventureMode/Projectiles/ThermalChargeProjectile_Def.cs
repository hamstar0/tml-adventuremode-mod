using System;
using Terraria;


namespace AdventureMode.Projectiles {
	public partial class ThermalChargeProjectile : BaseExplosiveChargeProjectile {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Thermal Charge" );

			this.drawOriginOffsetX = 0;
			this.drawOriginOffsetY = -8;
		}
	}
}
