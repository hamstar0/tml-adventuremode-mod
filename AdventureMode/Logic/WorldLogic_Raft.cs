using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Services.Hooks.LoadHooks;
using AdventureMode.WorldGeneration;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		private static int? _RaftBarrelRestockTimerSinceLastLoad = null;



		////////////////

		private static void BeginOrResumeRaftRestockTimer( int? remainingTime ) {
			int timer = remainingTime.HasValue
				? remainingTime.Value
				: AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			
			if( Timers.GetTimerTickDuration("AdventureModeRaftRestock") > 0 ) {
				return;
			}

			Timers.SetTimer( "AdventureModeRaftRestock", timer, false, () => {
				if( Main.gameMenu && !Main.dedServ && Main.netMode != NetmodeID.Server ) {
					return 0;
				}

				if( WorldLogic.RestockRaft() ) {
					Main.NewText( "Raft barrel has received new items!", Color.Lime );
				} else {
					Main.NewText( "No barrel to restock.", Color.Yellow );
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
				LogHelpers.Alert( "World has no raft barrel." );
			}

			LoadHooks.AddPostWorldLoadEachHook( () => {
				WorldLogic.BeginOrResumeRaftRestockTimer( WorldLogic._RaftBarrelRestockTimerSinceLastLoad );
				WorldLogic._RaftBarrelRestockTimerSinceLastLoad = null;
			} );
		}

		private static void LoadRaftMirror( AMWorld myworld, TagCompound tag ) {
			if( !tag.ContainsKey("raft_mirror_x") ) {
				LogHelpers.Alert( "World has no raft mirror." );
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
				LogHelpers.Warn( "Could not save raft info." );
				return;
			}

			int restockTicks = Timers.GetTimerTickDuration( "AdventureModeRaftRestock" );

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
