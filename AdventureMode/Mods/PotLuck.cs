using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using PotLuck;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPotLuck() {
			int randGemPick() {
				(int itemType, float weight)[] gemChances = new (int, float)[] {
					(ItemID.Amethyst, 1f / 3.75f),
					(ItemID.Topaz, 1f / 7.5f),
					(ItemID.Sapphire, 1f / 11.25f),
					(ItemID.Emerald, 1f / 15f),
					(ItemID.Ruby, 1f / 22.5f),
					(ItemID.Diamond, 1f / 30f),
					(ItemID.Amber, 1f / 45f)
				};

				float sum = gemChances.Sum( kv => kv.weight );
				float chance = Main.rand.NextFloat() * sum;

				float cumul = 0f;
				for( int i = 0; i < gemChances.Length; i++ ) {
					cumul += gemChances[i].weight;

					if( cumul > chance ) {
						return gemChances[i].itemType;
					}
				}

				return ItemID.Amethyst;
			}

			//

			bool gemDrop( int tileX, int tileY, out IList<int> droppedItemIndexes ) {
				var config = AMConfig.Instance;

				if( Main.rand.NextFloat() > config.PotGemPercentChance ) {
					droppedItemIndexes = new List<int>();
					return true;
				}

				//

				int who = Item.NewItem(
					X: tileX * 16,
					Y: tileY * 16,
					Width: 12,
					Height: 12,
					Type: randGemPick()
				);

				droppedItemIndexes = new List<int> { who };
				return true;
			}


			//

			PotLuckAPI.AddPotBreakAction( gemDrop );
		}
	}
}
