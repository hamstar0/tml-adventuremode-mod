using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeProjectile : GlobalProjectile {
		internal static void InitializeSingleton() {
			var projSingleton = ModContent.GetInstance<AdventureModeProjectile>();
			projSingleton.PlayerMagicProjectiles = new HashSet<int>();
		}



		////////////////

		private bool IsAccountedFor = false;
		private ISet<int> PlayerMagicProjectiles = null;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		private void Initialize( Projectile projectile ) {
			if( !projectile.npcProj && projectile.magic ) {
				var projSingleton = ModContent.GetInstance<AdventureModeProjectile>();
				projSingleton.PlayerMagicProjectiles.Add( projectile.whoAmI );
			}
		}


		////////////////

		public IEnumerable<int> GetAllPlayerMagicProjectiles() {
			var projWhos = new List<int>( this.PlayerMagicProjectiles.Count );

			foreach( int i in this.PlayerMagicProjectiles.ToArray() ) {
				Projectile proj = Main.projectile[i];
				if( proj == null || !proj.active || proj.npcProj || !proj.magic ) {
					continue;
				}

				this.PlayerMagicProjectiles.Remove( i );
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
