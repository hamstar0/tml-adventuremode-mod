using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using AdventureMode.Packets;
using AdventureMode.Data;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		private static void BeginOrResumeRaftRestockTimer_Host( int? remainingTicks ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			int timerTicks = remainingTicks.HasValue
				? remainingTicks.Value
				: AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;

			if( Main.netMode != NetmodeID.Server ) {
//LogLibraries.Log( "RAFT TIMER 1 "+timerTicks );
				WorldLogic.DeclareTimerHUD( timerTicks );
			}
			
			if( RaftModData.Instance.RaftRestockTimerSnapshot <= 0 ) {
				Timers.SetTimer(
					name: RaftModData.RaftRestockTimerName,
					tickDuration: timerTicks,
					runsWhilePaused: false,
					func: () => {
						if( Main.netMode == NetmodeID.MultiplayerClient ) {
							return 0;   // end timer if player transitions from SP to MP (redundant?)
						}
						if( Main.gameMenu && !Main.dedServ ) {
							return 0;   // end timer if in menu (redundant?)
						}

						WorldLogic.RestockRaftBarrelAndAlert();

						return WorldLogic.GetAndSetRestockTimerTicks();
					}
				);
			}
		}


		////

		private static void RestockRaftBarrelAndAlert() {
			WorldLogic.RestockRaftIf( out string msg, out Color color );
			
			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.BroadcastChatMessage( NetworkText.FromLiteral(msg), color, -1 );
			} else {
				Main.NewText( msg, color );
			}
		}

		private static int GetAndSetRestockTimerTicks() {
			int restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;

			if( Main.netMode == NetmodeID.SinglePlayer ) {
//LogLibraries.Log( "RAFT TIMER 2 "+restockTicks );
				WorldLogic.DeclareTimerHUD( restockTicks );
			} else if( Main.netMode == NetmodeID.Server ) {
				RaftRestockTimerPacket.SendToClient( restockTicks, -1 );
			}

			//

			return restockTicks;
		}


		////////////////
		
		 private static int _SyncRaftTimerTimer = 60 * 15;

		internal static void UpdateRaftRestockTimerSyncIf( int playerWho ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			//

			if( WorldLogic._SyncRaftTimerTimer-- >= 1 ) {
				return;
			}

			WorldLogic._SyncRaftTimerTimer = 60 * 15;

			//

			int ticks = RaftModData.Instance.RaftRestockTimerSnapshot;

			if( Main.netMode == NetmodeID.Server ) {
				RaftRestockTimerPacket.SendToClient( ticks, playerWho );
			} else if( Main.netMode == NetmodeID.SinglePlayer ) {
				WorldLogic.DeclareTimerHUD( ticks );
			}
		}


		////////////////

		internal static void UpdateRaftRestockTimerSnapshot() {
			int ticks = Timers.GetTimerTickDuration( RaftModData.RaftRestockTimerName );

			if( ticks > 0 ) {
				RaftModData.Instance.RaftRestockTimerSnapshot = ticks;
			}
		}
	}
}
