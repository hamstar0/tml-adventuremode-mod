using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Libraries.World;
using ModLibsGeneral.Services.Hooks.ExtendedHooks;
using ModLibsCore.Services.Timers;
using FindableManaCrystals.Tiles;


namespace AdventureMode.Logic {
	static partial class TileLogic {
		public static void InitializeTileKillBehaviors() {
			void killWall( int i, int j, int type, ref bool fail, bool nonGameplay ) {
				fail = !nonGameplay;
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( TileLogic.OnKillTile );
			var killWallHook = new ExtendedTileHooks.KillWallDelegate( killWall );

			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
			ExtendedTileHooks.AddSafeWallKillHook( killWallHook );
		}


		////////////////

		private static void OnKillTile(
					int i,
					int j,
					int type,
					ref bool fail,
					ref bool effectOnly,
					ref bool noItem,
					bool nonGameplay ) {
			if( Main.gameMenu && Main.netMode != NetmodeID.Server ) {
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
			if( Main.netMode != NetmodeID.Server && !LoadLibraries.IsCurrentPlayerInGame() ) {
				return;
			}
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}
			
			// Arachnophobes, rejoice!
			if( type == TileID.Pots ) {
				TileLogic.KillPotTile( i, j, ref noItem );
			}
		}


		private static void KillPotTile( int i, int j, ref bool noItem ) {
			// No spiders in underworld
			if( j >= WorldLocationLibraries.UnderworldLayerTopTileY ) {
				return;
			}

			UnifiedRandom rand = TmlLibraries.SafelyGetRand();
			if( rand.NextFloat() >= AMConfig.Instance.PotSurprisePercentChance ) {
				return;
			}
			
			TileLogic.CreatePotSpider( i, j, ref noItem );
		}


		private static void CreatePotSpider( int i, int j, ref bool noItem ) {
			void modifySpiderNpc( NPC spiderNpc ) {
				spiderNpc.life = spiderNpc.lifeMax / 6;
				spiderNpc.damage /= 2;
				spiderNpc.scale = 0.75f;
				spiderNpc.width = (int)( spiderNpc.width * 0.75f );
				spiderNpc.height = (int)( spiderNpc.height * 0.75f );
				spiderNpc.netUpdate = true;
			}

			//

			UnifiedRandom rand = TmlLibraries.SafelyGetRand();
			int spiderType = NPCID.WallCreeper;

			if( Main.hardMode && rand.NextBool() ) {
				spiderType = NPCID.BlackRecluse;
			}

			int npcWho = NPC.NewNPC( i * 16, ( j + 1 ) * 16, spiderType );
			NPC npc = Main.npc[npcWho];
			modifySpiderNpc( npc );

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
			}

			Timers.SetTimer( 4, false, () => {
				npc = Main.npc[npcWho];
				if( npc?.active != true || npc.type != spiderType ) {
					return false;
				}

				modifySpiderNpc( npc );
				
				if( Main.netMode == NetmodeID.Server ) {
					NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0 );
				}
				return false;
			} );

			noItem = true;
		}
	}
}
