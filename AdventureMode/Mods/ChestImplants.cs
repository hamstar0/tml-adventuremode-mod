using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using ChestImplants;
using MountedMagicMirrors.Items;
using Orbs.Items;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private static ChestImplanterDefinition MakeItemImplanter(
					int itemType,
					int quantity,
					float chancePerChest = 1f,
					string chestType = "Vanilla Underground World Chest" ) {
			var itemDef = new ChestImplanterItemDefinition {
				ChestItem = new ItemDefinition( itemType ),
				MinQuantity = quantity,
				MaxQuantity = quantity,
				ChancePerChest = chancePerChest,
			};

			return new ChestImplanterDefinition {
				ChestTypes = new List<Ref<string>> { new Ref<string>( chestType ) },
				ItemDefinitions = new List<ChestImplanterItemDefinition> { itemDef }
			};
		}


		private static ChestImplanterDefinition MakeItemImplanter(
					(int itemType, int quantity, float chancePerChest)[] defs,
					string chestType = "Vanilla Underground World Chest" ) {
			var itemDefinitions = new List<ChestImplanterItemDefinition>();

			foreach( (int itemType, int quantity, float chancePerChest) in defs ) {
				var itemDef = new ChestImplanterItemDefinition {
					ChestItem = new ItemDefinition( itemType ),
					MinQuantity = quantity,
					MaxQuantity = quantity,
					ChancePerChest = chancePerChest,
				};
				itemDefinitions.Add( itemDef );
			}

			return new ChestImplanterDefinition {
				ChestTypes = new List<Ref<string>> { new Ref<string>( "Vanilla Underground World Chest" ) },
				ItemDefinitions = itemDefinitions
			};
		}



		////////////////

		public void LoadChestImplants() {
			var implantsConfig = ChestImplantsConfig.Instance;
			var implanterDefs = new ChestImplanterSetDefinition( new List<Ref<ChestImplanterDefinition>>() );

			//

			void addItemImplanter( int itemType, int quantity, float chancePerChest=1f ) {
				implanterDefs.Value.Add(
					new Ref<ChestImplanterDefinition>( AdventureModeModInteractions.MakeItemImplanter(itemType, quantity, chancePerChest) )
				);
			}

			void addItemImplanter2( (int itemType, int quantity, float chancePerChest)[] defs, string chestType ) {
				implanterDefs.Value.Add(
					new Ref<ChestImplanterDefinition>( AdventureModeModInteractions.MakeItemImplanter(defs, chestType) )
				);
			}

			//

			if( !AMConfig.Instance.EnableAlchemyRecipes ) {
				addItemImplanter( ItemID.Bottle, -1 );
			}
			if( AMConfig.Instance.WorldGenRemoveMagicMirrors ) {
				addItemImplanter( ItemID.MagicMirror, -1 );
				addItemImplanter( ItemID.IceMirror, -1 );
			}
			addItemImplanter( ItemID.LivingWoodWand, -1 );
			addItemImplanter( ItemID.LeafWand, -1 );
			addItemImplanter( ItemID.LivingMahoganyWand, -1 );
			addItemImplanter( ItemID.LivingMahoganyLeafWand, -1 );
			addItemImplanter( ItemID.LivingMahoganyLeafWand, -1 );

			if( AMConfig.Instance.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				addItemImplanter(
					itemType: ModContent.ItemType<MountableMagicMirrorTileItem>(),
					quantity: 1,
					chancePerChest: AMConfig.Instance.WorldGenAddedMountedMagicMirrorChance
				);
			}

			addItemImplanter2( new (int, int, float)[] {
				(ItemID.ClimbingClaws, -1, 1f),
				(ModContent.ItemType<BlueOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<CyanOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<GreenOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<PinkOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<PurpleOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<RedOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<YellowOrbItem>(), 1, 1f / 8f),
				(ModContent.ItemType<WhiteOrbItem>(), 1, 1f / 8f)
			}, "Chest" );

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
