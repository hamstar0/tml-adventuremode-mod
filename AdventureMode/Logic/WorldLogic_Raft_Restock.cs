using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using AdventureMode.Packets;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public const string RaftRestockTimerName = "AdventureModeRaftRestock";



		////////////////

		public static int GetRaftRestockTimerTicks() {
			return AMMod.Instance.RaftRestockTimerSnapshot;
		}


		////////////////

		private static void BeginOrResumeRaftRestockTimerIf( int? remainingTicks ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			int timerTicks = remainingTicks.HasValue
				? remainingTicks.Value
				: AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;

			if( Main.netMode != NetmodeID.Server ) {
				WorldLogic.InititalizeTimerHUD( timerTicks );
			}
			
			if( WorldLogic.GetRaftRestockTimerTicks() <= 0 ) {
				Timers.SetTimer(
					name: WorldLogic.RaftRestockTimerName,
					tickDuration: timerTicks,
					runsWhilePaused: false,
					action: WorldLogic.RestockRaftBarrelAndAlertAndGetAndSetRestockTimerTicks
				);
			}
		}


		private static int RestockRaftBarrelAndAlertAndGetAndSetRestockTimerTicks() {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return 0;   // end timer if player transitions from SP to MP (redundant?)
			}
			if( Main.gameMenu && !Main.dedServ ) {
				return 0;   // end timer if in menu (redundant?)
			}

			//

			WorldLogic.RestockRaftIf( out string msg, out Color color );
			
			//

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.BroadcastChatMessage( NetworkText.FromLiteral(msg), color, -1 );
			} else {
				Main.NewText( msg, color );
			}

			//
			
			int restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;

			if( Main.netMode == NetmodeID.SinglePlayer ) {
				AMMod.Instance.RaftTimerHUD.SetTimerTicks( restockTicks );
			} else if( Main.netMode == NetmodeID.Server ) {
				RaftRestockTimerPacket.SendToClient( restockTicks, -1 );
			}

			//

			return restockTicks;
		}


		////
		
		internal static void UpdateRaftRestockTimerSnapshot() {
			int ticks = Timers.GetTimerTickDuration( WorldLogic.RaftRestockTimerName );

			if( ticks > 0 ) {
				AMMod.Instance.RaftRestockTimerSnapshot = ticks;
			}
		}
	}
}
