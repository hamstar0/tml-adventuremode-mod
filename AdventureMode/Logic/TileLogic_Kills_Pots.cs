using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;
using ModLibsGeneral.Libraries.World;
using ModLibsGeneral.Libraries.NPCs;


namespace AdventureMode.Logic {
	static partial class TileLogic {
		private static void KillPotTile( int i, int j, ref bool noItem ) {
			// No pot spawns in underworld
			if( j >= WorldLocationLibraries.UnderworldLayerTopTileY ) {
				return;
			}

			//

			var config = AMConfig.Instance;
			UnifiedRandom rand = TmlLibraries.SafelyGetRand();

			//

			if( rand.NextFloat() < config.PotSurprisePercentChance ) {
				TileLogic.CreatePotSpider( i, j, ref noItem );
			}
			
			if( rand.NextFloat() < config.PotWraithSpawnPercentChance ) {
				var mynpc = ModContent.GetInstance<AMNPC>();

				mynpc.QueuedExclusiveSpawns.Add( NPCID.Wraith );
			}
		}


		////////////////

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

			//

			int npcWho = NPC.NewNPC( i * 16, ( j + 1 ) * 16, spiderType );
			NPC npc = Main.npc[npcWho];
			modifySpiderNpc( npc );

			//

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
			}

			//

			Timers.SetTimer( 4, false, () => {
				npc = Main.npc[npcWho];
				if( npc?.active != true || npc.netID != spiderType ) {
					return false;
				}

				modifySpiderNpc( npc );

				//
				
				if( Main.netMode == NetmodeID.Server ) {
					NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0 );
				}

				return false;
			} );

			//

			noItem = true;
		}
	}
}
