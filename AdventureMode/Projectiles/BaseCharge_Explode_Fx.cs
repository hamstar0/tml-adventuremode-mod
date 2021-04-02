using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode.Projectiles {
	public abstract partial class BaseExplosiveChargeProjectile : ModProjectile {
		public override void Kill( int timeLeft ) {
			Projectile proj = this.projectile;
			Main.PlaySound( SoundID.Item14, proj.position );

			for( int i = 0; i < 20; i++ ) {
				int dustId = Dust.NewDust( new Vector2( proj.position.X, proj.position.Y ), proj.width, proj.height, 31, 0f, 0f, 100, default( Color ), 1.5f );
				Main.dust[dustId].velocity *= 1.4f;
			}

			for( int i = 0; i < 10; i++ ) {
				int dustId = Dust.NewDust( proj.position, proj.width, proj.height, 6, 0f, 0f, 100, default( Color ), 2.5f );
				Main.dust[dustId].noGravity = true;
				Main.dust[dustId].velocity *= 5f;
				dustId = Dust.NewDust( proj.position, proj.width, proj.height, 6, 0f, 0f, 100, default( Color ), 1.5f );
				Main.dust[dustId].velocity *= 3f;
			}

			int goreId = Gore.NewGore( proj.position, default, Main.rand.Next( 61, 64 ), 1f );
			Gore gore1 = Main.gore[goreId];
			gore1.velocity *= 0.4f;
			gore1.velocity.X += 1f;
			gore1.velocity.Y += 1f;

			goreId = Gore.NewGore( proj.position, default, Main.rand.Next( 61, 64 ), 1f );
			Gore gore2 = Main.gore[goreId];
			gore2.velocity *= 0.4f;
			gore2.velocity.X -= 1f;
			gore2.velocity.Y += 1f;

			goreId = Gore.NewGore( proj.position, default, Main.rand.Next( 61, 64 ), 1f );
			Gore gore3 = Main.gore[goreId];
			gore3.velocity *= 0.4f;
			gore3.velocity.X += 1f;
			gore3.velocity.Y -= 1f;

			goreId = Gore.NewGore( proj.position, default, Main.rand.Next( 61, 64 ), 1f );
			Gore gore4 = Main.gore[goreId];
			gore4.velocity *= 0.4f;
			gore4.velocity.X -= 1f;
			gore4.velocity.Y -= 1f;

			if( timeLeft == 0 ) {
				int tileX = (int)this.projectile.Center.X / 16;
				int tileY = (int)this.projectile.Center.Y / 16;

				this.CreateExplosion( tileX, tileY );

				if( Main.netMode == NetmodeID.Server ) {
					NetMessage.SendData( MessageID.KillProjectile, -1, -1, null, proj.identity, (float)proj.owner, 0f, 0f, 0, 0, 0 );
				}
			}
		}
	}
}
