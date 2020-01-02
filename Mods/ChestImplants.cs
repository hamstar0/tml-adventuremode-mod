using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using ChestImplants;
using MountedMagicMirrors.Items;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadChestImplants() {
			var implantsConfig = new ChestImplantsConfig();
			implantsConfig.AllFromSetChestImplanterDefinitions.Value.Clear();
			implantsConfig.ClearRandomImplanterSets();

			void addRemovedItemImplanter( int itemType ) {
				var itemDef = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( itemType ),
					MinQuantity = -1,
					MaxQuantity = -1,
					ChancePerChest = 1f,
				};
				var def = new ChestImplanterDefinition {
					ChestTypes = new List<Ref<string>> { new Ref<string>( "Vanilla Underground World Chest" ) },
					ItemDefinitions = new List<ChestImplanterItemDefinition> { itemDef }
				};

				implantsConfig.AllFromSetChestImplanterDefinitions.Value.Add(
					new Ref<ChestImplanterDefinition>( def )
				);
			}

			if( AdventureModeConfig.Instance.WorldGenRemoveMagicMirrors ) {
				addRemovedItemImplanter( ItemID.MagicMirror );
				addRemovedItemImplanter( ItemID.IceMirror );
			}
			addRemovedItemImplanter( ItemID.LivingWoodWand );
			addRemovedItemImplanter( ItemID.LeafWand );
			addRemovedItemImplanter( ItemID.LivingMahoganyWand );
			addRemovedItemImplanter( ItemID.LivingMahoganyLeafWand );

			if( AdventureModeConfig.Instance.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				var randItemDef = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( ModContent.ItemType<MountableMagicMirrorTileItem>() ),
					MinQuantity = 1,
					MaxQuantity = 1,
					ChancePerChest = 0.05f,
				};
				var randDef = new ChestImplanterDefinition {
					ChestTypes = new List<Ref<string>> { new Ref<string>( "Vanilla Underground World Chest" ) },
					ItemDefinitions = new List<ChestImplanterItemDefinition> { randItemDef }
				};

				implantsConfig.RandomPickFromSetChestImplanterDefinitions1.Value.Add(
					new Ref<ChestImplanterDefinition>( randDef )
				);
			}

			ChestImplantsConfig.Instance.OverlayChanges( implantsConfig );
		}
	}
}
