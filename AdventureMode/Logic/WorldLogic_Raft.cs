using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModLibsCore.Services.Hooks.LoadHooks;
using AdventureMode.WorldGeneration;
using AdventureMode.Packets;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void LoadRaftInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			myworld.Raft = new RaftComponents();

			WorldLogic.LoadRaftBarrel( myworld, tag );
			WorldLogic.LoadRaftMirror( myworld, tag );
		}


		////

		private static void LoadRaftBarrel( AMWorld myworld, TagCompound tag ) {
			int? _restockTimerSinceLastLoad = null;

			//

			if( tag.ContainsKey("raft_barrel_x") ) {
				myworld.Raft.Barrel = (
					tag.GetInt( "raft_barrel_x" ),
					tag.GetInt( "raft_barrel_y" )
				);

				_restockTimerSinceLastLoad = tag.GetInt( "raft_barrel_restock_timer" );
			} else {
				LogLibraries.Alert( "World has no raft barrel." );
			}

			//

			LoadHooks.AddPostWorldLoadOnceHook( () => {
				if( _restockTimerSinceLastLoad != null ) {
					WorldLogic.BeginOrResumeRaftRestockTimerIf( _restockTimerSinceLastLoad );
				} else {
					LogLibraries.Warn( "No raft restock timer provided." );
				}
			} );
		}

		private static void LoadRaftMirror( AMWorld myworld, TagCompound tag ) {
			if( !tag.ContainsKey("raft_mirror_x") ) {
				LogLibraries.Alert( "World has no raft mirror." );
				return;
			}

			myworld.Raft.Mirror = (
				tag.GetInt("raft_mirror_x"),
				tag.GetInt("raft_mirror_y")
			);
		}


		////////////////

		public static void SaveRaftInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			if( !myworld.Raft.IsInitialized ) {
				LogLibraries.Warn( "Could not save raft info." );
				return;
			}

			int restockTicks = Timers.GetTimerTickDuration( WorldLogic.RaftRestockTimerName );
			//if( restockTicks <= 0 ) {
			//	restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			//}

			tag["raft_barrel_x"] = myworld.Raft.Barrel.TileX;
			tag["raft_barrel_y"] = myworld.Raft.Barrel.TileY;
			tag["raft_barrel_restock_timer"] = restockTicks;

			tag["raft_mirror_x"] = myworld.Raft.Mirror.TileX;
			tag["raft_mirror_y"] = myworld.Raft.Mirror.TileY;
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
	}
}
