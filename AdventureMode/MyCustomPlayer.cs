using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;
using Messages;
using AdventureMode.Logic;
using AdventureMode.Packets;


namespace AdventureMode {
	class AMCustomPlayerData {
		public static AMCustomPlayerData Initialize( Player player, object rawData ) {
			JObject jData = null;
			if( rawData != null ) {
				if( rawData is string ) {
					jData = (JObject)JsonConvert.DeserializeObject( rawData as string );
				} else if( rawData is JObject ) {
					jData = rawData as JObject;
				}
			}

			var data = jData != null
				? jData.ToObject<AMCustomPlayerData>()
				: new AMCustomPlayerData();

			if( !data.IsSetup ) {
				PlayerLogic.SetupInWorldSpawnInventory( player );

				data.IsSetup = true;
			}

			return data;
		}


		public bool IsSetup = false;
	}




	class AMCustomPlayer : CustomPlayerData {
		private AMCustomPlayerData Data;



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				this.OnEnter_CurrentPlayer( data );
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

		private void OnEnter_CurrentPlayer( object data ) {
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
			
			this.Data = AMCustomPlayerData.Initialize( this.Player, data );

			//

			MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
				MessagesAPI.GetMessage( "nihilism_init" )?.SetReadMessage();

				MessagesAPI.AddMessage(
					title: "Fishing disabled for Adventure Mode!",
					description: "Have fishing bait? Just sell it. Fishing is now disabled.",
					modOfOrigin: AMMod.Instance,
					alertPlayer: true,
					isImportant: false,
					parentMessage: MessagesAPI.GameInfoCategoryMsg,
					id: "AdventureModeFishingRemoved"
				);
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


		////

		protected override object OnExit() {
			return this.Data;
		}


		////////////////

		protected override void Update() {
			WorldLogic.UpdateRaftRestockTimerSyncIf( this.PlayerWho );
		}
	}
}
