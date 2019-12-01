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
			implantsConfig.AllFromSetChestImplanterDefinitions.Clear();
			implantsConfig.RandomPickFromSetChestImplanterDefinitions.Clear();

			if( AdventureModeConfig.Instance.WorldGenRemoveMagicMirrors ) {
				var mirrorGoneDef1 = new ChestImplanterDefinition {
					ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.MagicMirror ),
							MinQuantity = -1,
							MaxQuantity = -1,
							ChancePerChest = 1f,
						}
					}
				};
				var mirrorGoneDef2 = new ChestImplanterDefinition {
					ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.IceMirror ),
							MinQuantity = -1,
							MaxQuantity = -1,
							ChancePerChest = 1f,
						}
					}
				};

				implantsConfig.AllFromSetChestImplanterDefinitions[ "AdventureModeMirrorGone" ]
					= new ChestImplanterSetDefinition { mirrorGoneDef1, mirrorGoneDef2 };
			}

			if( AdventureModeConfig.Instance.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				implantsConfig.RandomPickFromSetChestImplanterDefinitions[ "AdventureModeMountedMirrors" ]
					= new ChestImplanterSetDefinition { new ChestImplanterDefinition {
						ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ModContent.ItemType<MountableMagicMirrorTileItem>() ),
								MinQuantity = 1,
								MaxQuantity = 1,
								ChancePerChest = 0.05f,
							}
						}
				} };
			}

			ChestImplantsConfig.Instance.OverlayChanges( implantsConfig );
		}
	}
}
