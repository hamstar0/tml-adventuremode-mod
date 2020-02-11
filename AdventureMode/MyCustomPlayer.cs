using System;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Services.Messages.Inbox;
using HamstarHelpers.Services.Timers;


namespace AdventureMode {
	class MyCustomPlayer : CustomPlayerData {
		protected override void OnEnter( object data ) {
			Timers.SetTimer( "AdventureModeDelayedNihilismInboxMessageRemove", 2 * 60, false, () => {
				string _;
				InboxMessages.ReadMessage( "nihilism_init", out _ );
				return false;
			} );
		}
	}
}
