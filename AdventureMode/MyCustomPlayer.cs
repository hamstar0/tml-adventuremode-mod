using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;
using Messages;
using AdventureMode.Logic;
using AdventureMode.Packets;


namespace AdventureMode {
	class AMCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				this.OnEnter_CurrentPlayer();
			}

			//

			if( Main.netMode == NetmodeID.Server ) {
				var myworld = ModContent.GetInstance<AMWorld>();
				if( myworld.Raft.IsInitialized ) {
					int ticks = WorldLogic.GetRaftRestockTimerTicks();

					RaftRestockTimerPacket.SendToClient( ticks, this.PlayerWho );
				} else {
					LogLibraries.Alert( "Cannoy sync raft barrel; not initialized!" );
				}
			}
		}


		private void OnEnter_CurrentPlayer() {
			var config = AMConfig.Instance;
			var myplayer = this.Player.GetModPlayer<AMPlayer>();
			var myworld = ModContent.GetInstance<AMWorld>();
			bool isNotAdventurer = myplayer.IsAdventurer || config.DebugModeSkipPlayerValidityCheck;
			bool isNotAdventureWorld = myworld.IsCurrentWorldAdventure || config.DebugModeSkipWorldValidityCheck;

			if( !isNotAdventurer ) {
				Main.NewText( "Your character is not initialized for Adventure Mode. Exiting to menu in 15 seconds...", Color.Yellow );
			}
			if( !isNotAdventureWorld ) {
				Main.NewText( "This world is not initialized for Adventure Mode. Exiting to menu in 15 seconds...", Color.Yellow );
			}

			//

			Timers.SetTimer( 2 * 60, false, () => {
				string _;
				MessagesAPI.GetMessage("nihilism_init")?.SetReadMessage();
				//InboxAPIMirrorsLibraries.ReadMessage( "nihilism_init", out _ );
				return false;
			} );

			//

			Timers.SetTimer( 15 * 60, true, () => {
				if( !isNotAdventurer || !isNotAdventureWorld ) {
					if( Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient ) {
						TmlLibraries.ExitToMenu( false );
					} else if( Main.netMode == NetmodeID.Server ) {
						TmlLibraries.ExitToDesktop( false );
					}
				}
				return false;
			} );
		}


		////////////////

		private int _SyncRaftTimerTimer = 60 * 15;

		protected override void Update() {
			if( Main.netMode == NetmodeID.Server ) {
				if( this._SyncRaftTimerTimer-- <= 0 ) {
					this._SyncRaftTimerTimer = 60 * 15;

					int ticks = WorldLogic.GetRaftRestockTimerTicks();

					RaftRestockTimerPacket.SendToClient( ticks, this.PlayerWho );
				}
			}
		}
	}
}
