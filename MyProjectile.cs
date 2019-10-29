using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		internal static void InitializeSingleton() {
			var projSingleton = ModContent.GetInstance<AdventureModeProjectile>();
			projSingleton.MagicProjectiles = new HashSet<int>();
		}



		////////////////

		private bool IsAccountedFor = false;
		private ISet<int> MagicProjectiles = null;


		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		private void Initialize( Projectile projectile ) {
			if( projectile.magic ) {
				var projSingleton = ModContent.GetInstance<AdventureModeProjectile>();
				projSingleton.MagicProjectiles.Add( projectile.whoAmI );
			}
		}


		////////////////

		public IEnumerable<int> GetAllMagicProjectiles() {
			var projWhos = new List<int>( this.MagicProjectiles.Count );

			foreach( int i in this.MagicProjectiles.ToArray() ) {
				Projectile proj = Main.projectile[i];
				if( proj == null || !proj.active || !proj.magic ) {
					continue;
				}

				this.MagicProjectiles.Remove( i );
				projWhos.Add( i );
			}

			return projWhos;
		}


		////////////////

		public override bool PreAI( Projectile projectile ) {
			if( this.IsAccountedFor || Main.netMode == 2 ) {
				return true;
			}
			this.IsAccountedFor = true;

			this.Initialize( projectile );

			return base.PreAI( projectile );
		}
	}
}
