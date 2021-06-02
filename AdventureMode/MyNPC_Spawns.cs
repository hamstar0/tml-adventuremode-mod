using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMNPC : GlobalNPC {
		private bool _IsFilteringSpawn = false;

		private NPCSpawnInfo? _CurrentSpawnInfo = null;



		////////////////

		private void InitializeSpawnHooks() {
			void SpawnWrap( On.Terraria.NPC.orig_SpawnNPC orig ) {
				this._IsFilteringSpawn = true;
				orig.Invoke();
				this._IsFilteringSpawn = false;
			}

			int NewNPCWrap( On.Terraria.NPC.orig_NewNPC orig,
						int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target ) {
				if( !this._IsFilteringSpawn || !this._CurrentSpawnInfo.HasValue ) {
					return orig.Invoke( X, Y, Type, Start, ai0, ai1, ai2, ai3, Target );
				}

				if( !this.CanSpawn(Type, this._CurrentSpawnInfo.Value) ) {
					return 200;
				}

				return orig.Invoke( X, Y, Type, Start, ai0, ai1, ai2, ai3, Target );
			}

			//

			On.Terraria.NPC.SpawnNPC += SpawnWrap;
			On.Terraria.NPC.NewNPC += NewNPCWrap;
		}


		////////////////
		
		public override void EditSpawnPool( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			this._CurrentSpawnInfo = spawnInfo;
		}


		////////////////

		private bool CanSpawn( int npcType, NPCSpawnInfo spawnInfo ) {
			switch( npcType ) {
			case NPCID.BoundGoblin:
				if( !NPCLogic.CanBoundGoblinSpawn(spawnInfo) ) {
					return false;
				}
				break;
			case NPCID.BoundMechanic:
				if( !NPCLogic.CanBoundMechanicSpawn(spawnInfo) ) {
					return false;
				}
				break;
			case NPCID.VoodooDemon:
				if( !NPCLogic.CanVoodooDemonSpawn(spawnInfo) ) {
					return false;
				}
				break;
			}
			return true;
		}
	}
}
