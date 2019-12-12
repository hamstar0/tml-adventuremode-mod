using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureMode {
	class AdventureModeExtendedTileHooks : ILoadable {
		private static void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			// Main menu
			if( Main.gameMenu ) {
				return;
			}
			// House creation
			if( AdventureModeMod.Instance.ModInteractions.IsCreatingHouse ) {
				noItem = true;
				return;
			}

			if( fail || effectOnly ) {
				return;
			}
			if( Main.netMode != 2 && !LoadHelpers.IsCurrentPlayerInGame() ) {
				return;
			}

			if( AdventureModeConfig.Instance.HardmodeBreakableDirt && Main.hardMode && type == TileID.Dirt ) {
				return;
			}
			
			if( !AdventureModeTile.IsKillable(type) ) {
				fail = true;
				effectOnly = true;
				noItem = true;
			}

			// Arachnophobes, rejoince!
			if( type == TileID.Pots ) {
				AdventureModeExtendedTileHooks.KillPotTile( i, j, ref noItem );
			}
		}


		private static void KillPotTile( int i, int j, ref bool noItem ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			if( rand.NextFloat() >= AdventureModeConfig.Instance.PotSurprisePercentChance ) {
				return;
			}

			noItem = true;

			if( Main.netMode != 1 ) {
				int spiderType = NPCID.WallCreeper;
				if( Main.hardMode && rand.NextBool() ) {
					spiderType = NPCID.BlackRecluse;
				}

				int npcWho = NPC.NewNPC( i << 4, ( j + 1 ) << 4, spiderType );
				NPC npc = Main.npc[npcWho];
				npc.netUpdate = true;
				npc.lifeMax = npc.life /= 2;
				npc.scale *= 0.5f;

				if( Main.netMode == 2 && npcWho < Main.npc.Length - 1 ) {
					NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
				}
			}
		}



		////////////////

		private AdventureModeExtendedTileHooks() { }

		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnModsUnload() {
		}

		void ILoadable.OnPostModsLoad() {
			void killWall( int i, int j, int type, ref bool fail ) {
				fail = true;
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( AdventureModeExtendedTileHooks.KillTile );
			var killWallHook = new ExtendedTileHooks.KillWallDelegate( killWall );

			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
			ExtendedTileHooks.AddSafeWallKillHook( killWallHook );
		}
	}




	partial class AdventureModeTile : GlobalTile {
		public static bool IsKillable( int tileType ) {
			return AdventureModeConfig.Instance.TileKillWhitelist.Contains( TileID.GetUniqueKey( tileType ) );
		}



		////////////////

		/*public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			bool fail = false, effectOnly = false, noItem = false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail;
		}*/


		/*public override bool Slope( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return false;
		}*/

		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			if( Main.gameMenu || !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return AdventureModeTile.IsKillable( type );
			//bool fail=false, effectOnly=false, noItem=false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}

		/*public override bool KillSound( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return AdventureModeTile.IsKillable( type );
			//bool fail = false, effectOnly = false, noItem = false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}*/
	}
}