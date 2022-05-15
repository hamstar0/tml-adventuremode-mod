﻿using System;
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
using ModLibsGeneral.Services.Messages.Simple;
using Messages;
using AdventureMode.Logic;
using AdventureMode.Packets;
using AdventureMode.Data;


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
				PlayerLogic.SetupSpawnInventoryInWorld( player );
				PlayerLogic.ApplyRecommendedInventorySortion( player );

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
LogLibraries.Log($"DEBUG OUT 1 {this.Player.name} {isCurrentPlayer}");
			if( isCurrentPlayer ) {
				this.OnEnter_CurrentPlayer( data );
			}

			//

			if( Main.netMode == NetmodeID.Server ) {
				var myworld = ModContent.GetInstance<AMWorld>();
				if( myworld.Raft.IsInitialized ) {
					int ticks = RaftModData.Instance.RaftRestockTimerSnapshot;

					RaftRestockTimerPacket.SendToClient( ticks, this.PlayerWho );
				} else {
					LogLibraries.Alert( "Cannoy sync raft barrel; not initialized!" );
				}

				//

				if( myworld.CachedPKEChestIdx != -1 ) {
					Chest chest = Main.chest[ myworld.CachedPKEChestIdx ];

					for( int i=0; i<chest.item.Length; i++ ) {
						NetMessage.SendData( MessageID.SyncChestItem, this.PlayerWho, -1, null, myworld.CachedPKEChestIdx, i );
					}
				} else {
					LogLibraries.Alert( "Cannoy sync PKE barrel; not initialized!" );
				}
			}
		}

		private void OnEnter_CurrentPlayer( object data ) {
			var config = AMConfig.Instance;
			var myplayer = this.Player.GetModPlayer<AMPlayer>();
			var myworld = ModContent.GetInstance<AMWorld>();

			//

			bool isAdventurer = myplayer.IsAdventurer;
			
LogLibraries.Log($"DEBUG OUT 2 {isAdventurer} {this.Player.name}");
			if( !isAdventurer ) {
				if( PlayerLogic.RetrofitPlayerInventory_If(this.Player) ) {
					isAdventurer = true;
					myplayer.IsAdventurer = true;
				}

				isAdventurer |= config.DebugModeSkipPlayerValidityCheck;

				if( !isAdventurer ) {
					Main.NewText( "Your character is not initialized for Adventure Mode. Exiting to menu in 15 seconds...", Color.Yellow );

					LogLibraries.Log( $"Could not validate {this.Player.name} for Adventure Mode." );
				}
			}

			//

			bool isAdventureWorld = myworld.IsCurrentWorldAdventure || config.DebugModeSkipWorldValidityCheck;

			if( !isAdventureWorld ) {
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

			if( !isAdventurer || !isAdventureWorld ) {
				int seconds = 15;

				Timers.SetTimer( 60, true, () => {
					seconds--;

					if( seconds <= 0 ) {
						if( Main.netMode != NetmodeID.Server ) {
							TmlLibraries.ExitToMenu( false );
						} else {
							TmlLibraries.ExitToDesktop( false );
						}
					} else {
						if( Main.netMode != NetmodeID.Server ) {
							SimpleMessage.PostMessage( "Exitting to menu in:", (seconds+1)+" seconds", 60 );
						}
					}

					return seconds > 0;
				} );
			}
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
