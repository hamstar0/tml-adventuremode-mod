using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Messages.Inbox;
using HamstarHelpers.Services.Timers;


namespace AdventureMode {
	class MyCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

			var myplayer = this.Player.GetModPlayer<AdventureModePlayer>();
			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			bool isNotAdventurer = myplayer.IsAdventurer;
			bool isNotAdventureWorld = myworld.IsAdventure;

			if( !isNotAdventurer ) {
				Main.NewText( "Your character is not initialized for Adventure Mode. Exiting to menu in 15 seconds...", Color.Yellow );
			}
			if( !isNotAdventureWorld ) {
				Main.NewText( "This world is not initialized for Adventure Mode. Exiting to menu in 15 seconds...", Color.Yellow );
			}

			Timers.SetTimer( 2 * 60, false, () => {
				string _;
				InboxMessages.ReadMessage( "nihilism_init", out _ );
				return false;
			} );

			Timers.SetTimer( 15 * 60, true, () => {
				if( !isNotAdventurer || !isNotAdventureWorld ) {
					if( Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient ) {
						TmlHelpers.ExitToMenu( false );
					} else if( Main.netMode == NetmodeID.Server ) {
						TmlHelpers.ExitToDesktop( false );
					}
				}
				return false;
			} );
		}
	}
}
