using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using HUDElementsLib;
using HUDElementsLib.Elements.Samples;
using AdventureMode.Packets;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public const string RaftRestockTimerName = "AdventureModeRaftRestock";



		////////////////

		public static int GetRaftRestockTimerTicks() {
			return Timers.GetTimerTickDuration( WorldLogic.RaftRestockTimerName );
		}


		////////////////

		internal static void InititalizeTimerHUD( int ticks ) {
			if( Main.netMode == NetmodeID.Server ) {
				RaftRestockTimerPacket.SendToClients( ticks );

				return;
			}

			if( AMMod.Instance.RaftTimerHUD == null ) {
				var dim = new Vector2( 176f, 52f );
				var pos = new Vector2(
					((float)Main.screenWidth - dim.X) * 0.5f,
					(float)Main.screenHeight - dim.Y - 32f
				);

				AMMod.Instance.RaftTimerHUD = new TimerHUD(
					pos: pos,
					dim: dim,
					title: "Time until raft restocks:",
					startTimeTicks: (long)ticks,
					showTicks: false,
					enabler: () => Main.playerInventory
				);

				HUDElementsLibAPI.AddWidget( AMMod.Instance.RaftTimerHUD );
			} else {
				AMMod.Instance.RaftTimerHUD.SetTimerTicks( (long)ticks );
			}

			AMMod.Instance.RaftTimerHUD.StartTimer();
		}
	}
}
