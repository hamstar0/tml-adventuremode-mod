using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;


namespace AdventureMode.Logic {
	static partial class WorldLogic {
		public static bool RestockRaftIf( out string msg, out Color color ) {
			var myworld = ModContent.GetInstance<AMWorld>();
			if( !myworld.Raft.IsInitialized ) {
				LogLibraries.Alert( "Raft barrel not initialized." );

				msg = "Raft barrel failed to initialize.";
				color = Color.Red;
				return false;
			}

			//

			int chestIdx = myworld.GetRaftBarrelChestIndex();
			if( chestIdx == -1 ) {
				LogLibraries.Alert( "No raft barrel found to restock!" );

				msg = "No raft barrel found to restock!";
				color = Color.Red;
				return false;
			}

			//

			ItemQuantityDefinition def = WorldLogic.PickItemForRaft();
			Item newItem = def.GetItem();
			Chest chest = Main.chest[ chestIdx ];

			//

			int emptyChestIdx = -1;

			for( int i=0; i<chest.item.Length; i++ ) {
				Item currItem = chest.item[i];
				if( currItem?.active == true ) {
					continue;
				}

				emptyChestIdx = i;
				break;
			}

			if( emptyChestIdx == -1 ) {
				msg = "Raft barrel cannot be restocked while full!";
				color = Color.Yellow;
				return false;
			}

			//

			chest.item[emptyChestIdx] = newItem;

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendData(
					msgType: MessageID.SyncChestItem,
					remoteClient: -1,
					ignoreClient: -1,
					text: null,
					number: chestIdx,
					number2: (float)emptyChestIdx
				);
			}

			msg = "Raft barrel has received new items!";
			color = Color.Lime;
			return true;
		}


		////

		private static ItemQuantityDefinition PickItemForRaft() {
			var config = AMConfig.Instance;

			float totalWeight = config.RaftBarrelRestockSelection.Sum( def => def.Weight );
			totalWeight += config.RaftBarrelRestockMagitechScrapChance;

			float randWeightPos = Main.rand.NextFloat( totalWeight );

			float countedWeights = 0;
			foreach( ItemQuantityDefinition def in config.RaftBarrelRestockSelection ) {
				countedWeights += def.Weight;
				if( countedWeights >= randWeightPos ) {
					return def;
				}
			}

			if( ModLoader.GetMod("RuinedItems") != null ) {
				if( config.RaftBarrelRestockMagitechScrapChance > 0f ) {
					return new ItemQuantityDefinition(
						mod: "RuinedItems",
						name: "MagitechScrapItem",
						quantity: 1
					);
				}
			}

			return null;
		}
	}
}
