using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static bool RestockRaft() {
			var myworld = ModContent.GetInstance<AMWorld>();
			if( !myworld.Raft.IsInitialized ) {
				LogHelpers.Alert( "Raft barrel not initialized." );
				return false;
			}

			int chestIdx = myworld.GetRaftBarrelChestIndex();
			if( chestIdx == -1 ) {
				LogHelpers.Alert( "No raft barrel found to restock!" );
				Main.NewText( "No raft barrel found to restock!", Color.Red );
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
