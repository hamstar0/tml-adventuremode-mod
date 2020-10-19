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
			var implantsConfig = ChestImplantsConfig.Instance;
			var implanterDefs = new ChestImplanterSetDefinition( new List<Ref<ChestImplanterDefinition>>() );

			//

			void addItemImplanter( int itemType, int quantity, float chancePerChest=1f ) {
				var itemDef = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( itemType ),
					MinQuantity = quantity,
					MaxQuantity = quantity,
					ChancePerChest = chancePerChest,
				};
				var def = new ChestImplanterDefinition {
					ChestTypes = new List<Ref<string>> { new Ref<string>( "Vanilla Underground World Chest" ) },
					ItemDefinitions = new List<ChestImplanterItemDefinition> { itemDef }
				};

				implanterDefs.Value.Add(
					new Ref<ChestImplanterDefinition>( def )
				);
			}

			//

			if( !AdventureModeConfig.Instance.EnableAlchemyRecipes ) {
				addItemImplanter( ItemID.Bottle, -1 );
			}
			if( AdventureModeConfig.Instance.WorldGenRemoveMagicMirrors ) {
				addItemImplanter( ItemID.MagicMirror, -1 );
				addItemImplanter( ItemID.IceMirror, -1 );
			}
			addItemImplanter( ItemID.LivingWoodWand, -1 );
			addItemImplanter( ItemID.LeafWand, -1 );
			addItemImplanter( ItemID.LivingMahoganyWand, -1 );
			addItemImplanter( ItemID.LivingMahoganyLeafWand, -1 );

			if( AdventureModeConfig.Instance.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				addItemImplanter(
					ModContent.ItemType<MountableMagicMirrorTileItem>(),
					1,
					AdventureModeConfig.Instance.WorldGenAddedMountedMagicMirrorChance
				);
			}

			////

			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.AllFromSetChestImplanterDefinitions),
				implanterDefs
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions1),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions2),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions3),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions4),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions5),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions6),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions7),
				new ChestImplanterSetDefinition()
			);
			implantsConfig.SetOverride(
				nameof(ChestImplantsConfig.RandomPickFromSetChestImplanterDefinitions8),
				new ChestImplanterSetDefinition()
			);
		}
	}
}
