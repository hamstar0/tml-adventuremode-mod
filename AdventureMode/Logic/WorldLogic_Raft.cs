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


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		private static int? _RaftBarrelRestockTimerSinceLastLoad = null;



		////////////////

		private static void BeginOrResumeRaftRestockTimer( int? remainingTicks ) {
			int timerTicks = remainingTicks.HasValue
				? remainingTicks.Value
				: AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;

			WorldLogic.InititalizeTimerHUD( timerTicks );
			
			if( Timers.GetTimerTickDuration(WorldLogic.RaftRestockTimerName) > 0 ) {
				return;
			}

			Timers.SetTimer( WorldLogic.RaftRestockTimerName, timerTicks, false, () => {
				if( Main.gameMenu && !Main.dedServ && Main.netMode == NetmodeID.MultiplayerClient ) {
					return 0;
				}

				string msg = "Raft barrel has received new items!";
				Color color;

				if( WorldLogic.RestockRaft() ) {
					msg = "Raft barrel has received new items!";
					color = Color.Lime;
				} else {
					msg = "No barrel to restock.";
					color = Color.Yellow;
				}

				if( Main.netMode == NetmodeID.Server ) {
					NetMessage.BroadcastChatMessage( NetworkText.FromLiteral(msg), color, -1 );
				} else {
					Main.NewText( msg, color );
				}

				return AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			} );
		}


		////////////////

		public static void LoadRaftInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AMWorld>();

			myworld.Raft = new RaftComponents();

			WorldLogic.LoadRaftBarrel( myworld, tag );
			WorldLogic.LoadRaftMirror( myworld, tag );
		}

		////

		private static void LoadRaftBarrel( AMWorld myworld, TagCompound tag ) {
			if( tag.ContainsKey("raft_barrel_x") ) {
				myworld.Raft.Barrel = (
					tag.GetInt( "raft_barrel_x" ),
					tag.GetInt( "raft_barrel_y" )
				);

				WorldLogic._RaftBarrelRestockTimerSinceLastLoad = tag.GetInt( "raft_barrel_restock_timer" );
			} else {
				LogLibraries.Alert( "World has no raft barrel." );
			}
			
			LoadHooks.AddPostWorldLoadEachHook( () => {
				if( Main.netMode != NetmodeID.MultiplayerClient ) {
					if( WorldLogic._RaftBarrelRestockTimerSinceLastLoad == null ) {
						LogLibraries.Warn( "No raft restock timer provided." );

						return;
					}

					WorldLogic.BeginOrResumeRaftRestockTimer( WorldLogic._RaftBarrelRestockTimerSinceLastLoad );
					WorldLogic._RaftBarrelRestockTimerSinceLastLoad = null;
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

			if( restockTicks <= 0 ) {
				restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			}

			tag["raft_barrel_x"] = myworld.Raft.Barrel.TileX;
			tag["raft_barrel_y"] = myworld.Raft.Barrel.TileY;
			tag["raft_barrel_restock_timer"] = restockTicks;

			tag["raft_mirror_x"] = myworld.Raft.Mirror.TileX;
			tag["raft_mirror_y"] = myworld.Raft.Mirror.TileY;
		}
	}
}
