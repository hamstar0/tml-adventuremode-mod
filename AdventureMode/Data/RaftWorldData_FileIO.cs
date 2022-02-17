using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Data {
	public partial class RaftWorldData {
		public void Load( TagCompound tag, out int? restockTimerSinceLastLoad ) {
			this.LoadBarrel( tag, out restockTimerSinceLastLoad );
			this.LoadMirror( tag );
		}


		////

		private void LoadBarrel( TagCompound tag, out int? restockTimerSinceLastLoad ) {
			if( tag.ContainsKey("raft_barrel_x") ) {
				this.Barrel = (
					tag.GetInt( "raft_barrel_x" ),
					tag.GetInt( "raft_barrel_y" )
				);

				restockTimerSinceLastLoad = tag.GetInt( "raft_barrel_restock_timer" );
			} else {
				restockTimerSinceLastLoad = null;

				LogLibraries.Alert( "World has no raft barrel." );
			}
		}

		private void LoadMirror( TagCompound tag ) {
			if( !tag.ContainsKey("raft_mirror_x") ) {
				LogLibraries.Alert( "World has no raft mirror." );
				return;
			}

			this.Mirror = (
				tag.GetInt("raft_mirror_x"),
				tag.GetInt("raft_mirror_y")
			);
		}


		////////////////

		public void Save( TagCompound tag ) {
			if( !this.IsInitialized ) {
				LogLibraries.Warn( "Could not save raft info." );
				return;
			}

			//if( restockTicks <= 0 ) {
			//	restockTicks = AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			//}

			tag["raft_barrel_x"] = this.Barrel.TileX;
			tag["raft_barrel_y"] = this.Barrel.TileY;
			tag["raft_barrel_restock_timer"] = RaftModData.Instance.RaftRestockTimerSnapshot;

			tag["raft_mirror_x"] = this.Mirror.TileX;
			tag["raft_mirror_y"] = this.Mirror.TileY;
		}
	}
}
