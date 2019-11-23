using HamstarHelpers.Helpers.Debug;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;


namespace AdventureMode {
	class AdventureModeWorld : ModWorld {
		internal ISet<string> IntroducedNpcUniqueKeys = new HashSet<string>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.IntroducedNpcUniqueKeys.Clear();

			if( !tag.ContainsKey("introduced_npc_count") ) {
				return;
			}

			int count = tag.GetInt( "introduced_npc_count" );

			for( int i=0; i<count; i++ ) {
				string uniqueKey = tag.GetString( "introduced_npc_"+i );
				this.IntroducedNpcUniqueKeys.Add( uniqueKey );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound { { "introduced_npc_count", this.IntroducedNpcUniqueKeys.Count } };

			int i = 0;
			foreach( string key in this.IntroducedNpcUniqueKeys ) {
				tag["introduced_npc_" + i] = key;
			}

			return tag;
		}


		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( pass => pass.Name.Equals("Grass Wall") );

			if( idx != -1 ) {
				tasks.Insert( idx + 1, new PassLegacy( "Adventure Mode: Set Default Spawn", ( progress ) => {
					AdventureModeWorldGen.SetBeachSpawn( progress );
					progress.Value = 1f;
				} ) );
				tasks.Insert( idx + 2, new PassLegacy( "Adventure Mode: Create Spawn Boat", ( progress ) => {
					AdventureModeWorldGen.PlaceRaft( progress );
					progress.Value = 1f;
				} ) );
			}
		}
	}
}
