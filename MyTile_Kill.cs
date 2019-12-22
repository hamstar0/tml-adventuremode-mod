﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using HamstarHelpers.Services.Timers;
using FindableManaCrystals.Tiles;


namespace AdventureMode {
	class AdventureModeExtendedTileHooks : ILoadable {
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

				Timers.SetTimer( "AdventureModePotSurprise", 2, false, () => {
					NPC npc = Main.npc[npcWho];
					if( !npc.active || npc.type != spiderType ) {
						return false;
					}

					npc.life /= 2;
					npc.lifeMax /= 2;
					npc.scale *= 0.5f;
					npc.netUpdate = true;

					if( Main.netMode == 2 && npcWho < Main.npc.Length - 1 ) {
						NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho, 0f, 0f, 0f, 0, 0, 0 );
					}
					return false;
				} );
			}
		}



		////////////////

		private AdventureModeExtendedTileHooks() { }

		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnModsUnload() {
		}

		void ILoadable.OnPostModsLoad() {
			void killWall( int i, int j, int type, ref bool fail, bool nonGameplay ) {
				fail = !nonGameplay;
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( AdventureModeExtendedTileHooks.OnKillTile );
			var killWallHook = new ExtendedTileHooks.KillWallDelegate( killWall );

			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
			ExtendedTileHooks.AddSafeWallKillHook( killWallHook );
		}
	}




	partial class AdventureModeTile : GlobalTile {
	}
}