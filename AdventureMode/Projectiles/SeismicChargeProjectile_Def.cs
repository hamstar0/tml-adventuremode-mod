using System;
using Terraria;


namespace AdventureMode.Projectiles {
	public partial class SeismicChargeProjectile : BaseExplosiveChargeProjectile {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Seismic Charge" );

			this.drawOriginOffsetX = 0;
			this.drawOriginOffsetY = -8;
		}
	}
}
