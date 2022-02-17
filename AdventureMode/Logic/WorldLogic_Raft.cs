using System;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Hooks.LoadHooks;
using AdventureMode.Data;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void LoadRaftData( AMWorld myworld, TagCompound tag ) {
			myworld.Raft = new RaftWorldData();

			myworld.Raft.Load( tag, out int? restockTimerSinceLastLoad );

			//

			LoadHooks.AddPostWorldLoadOnceHook( () => {
				if( restockTimerSinceLastLoad.HasValue ) {
					WorldLogic.BeginOrResumeRaftRestockTimer_Host( restockTimerSinceLastLoad );
				} else {
					LogLibraries.Warn( "No raft restock timer provided." );
				}
			} );
		}


		public static void SaveRaftData( AMWorld myworld, TagCompound tag ) {
			myworld.Raft.Save( tag );
		}


		////////////////

		public static void NetReceiveRaftData( AMWorld myworld, BinaryReader reader ) {
			myworld.Raft.NetReceive( reader );
		}

		public static void NetSendRaftData( AMWorld myworld, BinaryWriter writer ) {
			myworld.Raft.NetSend( writer );
		}
	}
}
