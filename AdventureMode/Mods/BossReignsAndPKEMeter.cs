using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Hooks.LoadHooks;
using ModLibsCore.Services.Timers;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadBossReignsAndPKEMeter() {
			BossReigns.BossReignsAPI.SetReignBuildupPause( true );

			LoadHooks.AddSafeWorldLoadEachHook(
				AdventureModeModInteractions.LoadBossReignsAndPKEMeter_WorldInPlay_UpdateBossReignsUnpauseState
			);
		}


		////////////////

		private static void LoadBossReignsAndPKEMeter_WorldInPlay_UpdateBossReignsUnpauseState() {
			bool Timer_CanUpdate() {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					return false;
				}

				if( !BossReigns.BossReignsAPI.IsReignBuildupPaused() ) {
					return false;
				}

				//

				bool canUnpause = AdventureModeModInteractions.LoadBossReignsAndPKEMeter_CanBossReignsUnpausee();

				if( canUnpause ) {
					BossReigns.BossReignsAPI.SetReignBuildupPause( false );
				}

				return !canUnpause;
			}

			//

			Timers.SetTimer( "AMReignBeginChecker", 60, false, Timer_CanUpdate );
		}


		////

		private static bool LoadBossReignsAndPKEMeter_CanBossReignsUnpausee() {
			int pkeMeterType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			//

			foreach( Player plr in Main.player ) {
				if( plr?.active != true ) {
					continue;
				}

				if( plr.inventory.Any( i => i?.active == true && i.type == pkeMeterType ) ) {
					return true;
				}
			}

			return false;
		}
	}
}
