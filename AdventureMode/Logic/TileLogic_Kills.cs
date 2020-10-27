using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using HamstarHelpers.Services.Timers;
using FindableManaCrystals.Tiles;


namespace AdventureMode.Logic {
	static partial class TileLogic {
		private static void OnKillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem, bool nonGameplay ) {
			if( Main.gameMenu ) {
				return;
			}
			if( noItem ) {
				return;
			}
			if( nonGameplay ) {
				noItem = type != TileID.Pots && type != TileID.Heart && type != ModContent.TileType<ManaCrystalShardTile>();
				return;
			}

			if( fail || effectOnly ) {
				return;
			}
			if( Main.netMode != 2 && !LoadHelpers.IsCurrentPlayerInGame() ) {
				return;
			}

			// Arachnophobes, rejoince!
			if( type == TileID.Pots ) {
				TileLogic.KillPotTile( i, j, ref noItem );
			}
		}


		private static void KillPotTile( int i, int j, ref bool noItem ) {
			if( Main.netMode == 1 ) {
				return;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			if( rand.NextFloat() >= AdventureModeConfig.Instance.PotSurprisePercentChance ) {
				return;
			}

			//

			void modifySpiderNpc( NPC spiderNpc ) {
				spiderNpc.life = spiderNpc.lifeMax / 4;
				spiderNpc.damage = spiderNpc.damage / 2;
				spiderNpc.scale = 0.75f;
				spiderNpc.width = (int)( spiderNpc.width * 0.75f );
				spiderNpc.height = (int)( spiderNpc.height * 0.75f );
				spiderNpc.netUpdate = true;
			}

			//

			noItem = true;

			int spiderType = NPCID.WallCreeper;
			if( Main.hardMode && rand.NextBool() ) {
				spiderType = NPCID.BlackRecluse;
			}

			int npcWho = NPC.NewNPC( i * 16, ( j + 1 ) * 16, spiderType );
			NPC npc = Main.npc[npcWho];
			modifySpiderNpc( npc );

			Timers.SetTimer( "AdventureModePotSurprise", 4, false, () => {
				npc = Main.npc[npcWho];
				if( npc?.active != true || npc.type != spiderType ) {
					return false;
				}

				modifySpiderNpc( npc );

				if( Main.netMode != 0 ) {
					NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
				}
				return false;
			} );
		}



		////////////////

		public static void InitializeTileKillBehaviors() {
			void killWall( int i, int j, int type, ref bool fail, bool nonGameplay ) {
				fail = !nonGameplay;
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( TileLogic.OnKillTile );
			var killWallHook = new ExtendedTileHooks.KillWallDelegate( killWall );

			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
			ExtendedTileHooks.AddSafeWallKillHook( killWallHook );
		}
	}
}
