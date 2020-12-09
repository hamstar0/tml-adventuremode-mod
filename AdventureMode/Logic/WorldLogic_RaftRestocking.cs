using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static void LoadRaftRestockInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AMWorld>();
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
				if( Main.gameMenu && !Main.dedServ && Main.netMode != NetmodeID.Server ) {
					return 0;
				}

				if( WorldLogic.RestockRaft() ) {
					Main.NewText( "Raft barrel restocked.", Color.Lime );
				} else {
					Main.NewText( "No barrel to restock.", Color.Yellow );
				}

				return AMConfig.Instance.RaftBarrelRestockSecondsDuration * 60;
			} );
		}

		public static void SaveRaftRestockInfo( TagCompound tag ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			int restockTicks = Timers.GetTimerTickDuration( "AdventureModeRaftRestock" );

			tag["raft_x"] = myworld.RaftBarrelTile.TileX;
			tag["raft_y"] = myworld.RaftBarrelTile.TileY;
			tag["raft_restock_timer"] = restockTicks;
		}


		////////////////

		public static bool RestockRaft() {
			var myworld = ModContent.GetInstance<AMWorld>();
			int chestIdx = myworld.GetRaftBarrelChestIndex();
			if( chestIdx == -1 ) {
				LogHelpers.Alert( "No raft barrel found." );
				return false;
			}

			ItemQuantityDefinition def = WorldLogic.PickItemForRaft();
			Item newItem = def.GetItem();
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


		////

		private static ItemQuantityDefinition PickItemForRaft() {
			var config = AMConfig.Instance;
			float totalWeight = config.RaftBarrelRestockSelection.Sum( def => def.Weight );

			float randWeightPos = Main.rand.NextFloat( totalWeight );

			float countedWeights = 0;
			foreach( ItemQuantityDefinition def in config.RaftBarrelRestockSelection ) {
				countedWeights += def.Weight;
				if( countedWeights >= randWeightPos ) {
					return def;
				}
			}

			return null;
		}
	}
}
