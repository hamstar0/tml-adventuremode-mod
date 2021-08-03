﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;
using Messages;


namespace AdventureMode {
	class AMCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

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

			Timers.SetTimer( 2 * 60, false, () => {
				string _;
				MessagesAPI.GetMessage("nihilism_init")?.SetReadMessage();
				//InboxAPIMirrorsLibraries.ReadMessage( "nihilism_init", out _ );
				return false;
			} );

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
	}
}
