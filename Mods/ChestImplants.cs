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

			if( AdventureModeConfig.Instance.WorldGenRemoveMagicMirrors ) {
				var mirrorGoneItemDef1 = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( ItemID.MagicMirror ),
					MinQuantity = -1,
					MaxQuantity = -1,
					ChancePerChest = 1f,
				};
				var mirrorGoneItemDef2 = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( ItemID.IceMirror ),
					MinQuantity = -1,
					MaxQuantity = -1,
					ChancePerChest = 1f,
				};

				var mirrorGoneDef1 = new ChestImplanterDefinition {
					ChestTypes = new List<Ref<string>> { new Ref<string>("Vanilla Underground World Chest") },
					ItemDefinitions = new List<ChestImplanterItemDefinition> { mirrorGoneItemDef1 }
				};
				var mirrorGoneDef2 = new ChestImplanterDefinition {
					ChestTypes = new List<Ref<string>> { new Ref<string>("Vanilla Underground World Chest") },
					ItemDefinitions = new List<ChestImplanterItemDefinition> { mirrorGoneItemDef2 }
				};

				implantsConfig.AllFromSetChestImplanterDefinitions.Value.Add(
					new Ref<ChestImplanterDefinition>( mirrorGoneDef1 )
				);
				implantsConfig.AllFromSetChestImplanterDefinitions.Value.Add(
					new Ref<ChestImplanterDefinition>( mirrorGoneDef2 )
				);
			}

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
