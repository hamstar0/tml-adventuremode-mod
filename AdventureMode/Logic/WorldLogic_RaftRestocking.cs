using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void LoadRaftRestockInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			if( !tag.ContainsKey("raft_x") ) {
				LogHelpers.Alert( "World has no raft barrel." );
				return;
			}

			myworld.RaftBarrelTile = (
				tag.GetInt("raft_x"),
				tag.GetInt("raft_y")
			);

			int timer = tag.GetInt( "raft_restock_timer" );

			Timers.SetTimer( "AdventureModeRaftRestock", timer, false, () => {
				WorldLogic.RestockRaft();

				return AdventureModeConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			} );
		}

		public static void SaveRaftRestockInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			int restockTicks = Timers.GetTimerTickDuration( "AdventureModeRaftRestock" );

			tag["raft_x"] = myworld.RaftBarrelTile.TileX;
			tag["raft_y"] = myworld.RaftBarrelTile.TileY;
			tag["raft_restock_timer"] = restockTicks;
		}


		////////////////

		public static bool RestockRaft() {
			var config = AdventureModeConfig.Instance;
			int optionCount = config.RaftBarrelRestockSelection.Count;
			ItemQuantityDefinition def = config.RaftBarrelRestockSelection[ Main.rand.Next(optionCount) ];
			Item newItem = def.GetItem();

			var myworld = ModContent.GetInstance<AdventureModeWorld>();
			int chestIdx = myworld.GetRaftBarrelChestIndex();
			if( chestIdx == -1 ) {
				LogHelpers.Alert( "No raft barrel found." );
				return false;
			}

			Chest chest = Main.chest[ chestIdx ];

			for( int i=0; i<chest.item.Length; i++ ) {
				Item currItem = chest.item[i];
				if( currItem?.active == true ) {
					continue;
				}

				chest.item[i] = newItem;

				if( Main.netMode == NetmodeID.Server ) {
					NetMessage.SendData(
						msgType: MessageID.SyncChestItem,
						remoteClient: -1,
						ignoreClient: -1,
						text: null,
						number: chestIdx,
						number2: (float)i
					);
				}

				return true;
			}

			return false;
		}
	}
}
