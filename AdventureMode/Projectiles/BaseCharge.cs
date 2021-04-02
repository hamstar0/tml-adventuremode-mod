using System;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode.Projectiles {
	public abstract partial class BaseExplosiveChargeProjectile : ModProjectile {
		public override void SetDefaults() {
			this.projectile.width = 8;
			this.projectile.height = 8;
			this.projectile.aiStyle = 16;
			this.projectile.friendly = true;
			this.projectile.penetrate = -1;
			this.projectile.timeLeft = 180;
			this.projectile.damage = 0;
		}

		public override bool? CanCutTiles() {
			return false;
		}

		public override bool CanDamage() {
			return this.projectile.timeLeft <= 3;
		}


		////

		public override void AI() {
			var proj = this.projectile;

			if( proj.owner == Main.myPlayer && proj.timeLeft <= 3 ) {
				proj.tileCollide = false;
				proj.ai[1] = 0f;
				proj.alpha = 255;

				proj.position.X += (float)( proj.width / 2 );
				proj.position.Y += (float)( proj.height / 2 );

				proj.width = 128;
				proj.height = 128;
				proj.position.X -= (float)( proj.width / 2 );
				proj.position.Y -= (float)( proj.height / 2 );

				proj.damage = 100;
				proj.knockBack = 8f;
			}
		}
	}
}
