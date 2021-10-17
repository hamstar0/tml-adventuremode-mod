using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Hooks.LoadHooks;
using AdventureMode.WorldGeneration;


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
			int? restockTimerSinceLastLoad = null;

			//

			if( tag.ContainsKey("raft_barrel_x") ) {
				myworld.Raft.Barrel = (
					tag.GetInt( "raft_barrel_x" ),
					tag.GetInt( "raft_barrel_y" )
				);

				restockTimerSinceLastLoad = tag.GetInt( "raft_barrel_restock_timer" );
			} else {
				LogLibraries.Alert( "World has no raft barrel." );
			}

			//
			
			LoadHooks.AddPostWorldLoadOnceHook( () => {
				if( restockTimerSinceLastLoad != null ) {
					WorldLogic.BeginOrResumeRaftRestockTimerIf( restockTimerSinceLastLoad );
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

			int restockTicks = WorldLogic.GetRaftRestockTimerTicks();
			//if( restockTicks <= 0 ) {
			//	restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			//}

			tag["raft_barrel_x"] = myworld.Raft.Barrel.TileX;
			tag["raft_barrel_y"] = myworld.Raft.Barrel.TileY;
			tag["raft_barrel_restock_timer"] = restockTicks;

			tag["raft_mirror_x"] = myworld.Raft.Mirror.TileX;
			tag["raft_mirror_y"] = myworld.Raft.Mirror.TileY;
		}
	}
}
