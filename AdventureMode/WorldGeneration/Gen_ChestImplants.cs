using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using MountedMagicMirrors.Items;
using Orbs.Items;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		private static void AddItemToWorldChests( int itemType, int quantity, float chancePerChest = 1f ) {
			var fillDef = new ChestFillDefinition(
				single: new ChestFillItemDefinition( itemType, quantity, quantity ),
				percentChance: chancePerChest
			);

			WorldChestLibraries.AddToWorldChests( fillDef );
		}

		
		private static void RemoveItemFromWorldChests( int itemType, float chancePerChest = 1f ) {
			var fillDef = new ChestFillDefinition(
				single: new ChestFillItemDefinition( itemType, 1, 999 ),
				percentChance: chancePerChest
			);

			WorldChestLibraries.RemoveFromWorldChests( fillDef );
		}


		private static void ReplaceItemWithOtherInWorldChests( int itemType, int otherItemType ) {  //string chestType="Chest"
			var unfillDef = new ChestFillDefinition( new ChestFillItemDefinition( itemType, 1, 1 ) );
			var otherFillDef = new ChestFillDefinition( new ChestFillItemDefinition( otherItemType, 1, 1 ) );
			var chestDef = new ChestTypeDefinition( TileID.Containers, null );

			IList<Chest> modifiedChests = WorldChestLibraries.RemoveFromWorldChests( unfillDef, chestDef );
			foreach( Chest chest in modifiedChests ) {
				otherFillDef.Fill( chest );
			}
		}


		private static void ReplaceItemWithOrbsInWorldChests( int itemType ) {  //string chestType="Chest"
			var unfillDef = new ChestFillDefinition( new ChestFillItemDefinition( itemType, 1, 1 ) );
			var orbFillDef = new ChestFillDefinition( new (float weight, ChestFillItemDefinition def)[] {
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<BlueOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<CyanOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<GreenOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<PinkOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<PurpleOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<RedOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<YellowOrbItem>()) ),
					( 1f / 8f, new ChestFillItemDefinition(ModContent.ItemType<WhiteOrbItem>()) ),
				} );
			var chestDef = new ChestTypeDefinition( TileID.Containers, null );

			IList<Chest> modifiedChests = WorldChestLibraries.RemoveFromWorldChests( unfillDef, chestDef );
			foreach( Chest chest in modifiedChests ) {
				orbFillDef.Fill( chest );
			}
		}


		////////////////

		public static void LoadChestEdits() {
			var config = AMConfig.Instance;

			if( !config.EnableAlchemyRecipes ) {
				AMWorldGen.RemoveItemFromWorldChests( ItemID.Bottle );
			}
			if( config.WorldGenRemoveMagicMirrors ) {
				AMWorldGen.RemoveItemFromWorldChests( ItemID.MagicMirror );
				AMWorldGen.RemoveItemFromWorldChests( ItemID.IceMirror );
			}
			AMWorldGen.RemoveItemFromWorldChests( ItemID.LivingWoodWand );
			AMWorldGen.RemoveItemFromWorldChests( ItemID.LeafWand );
			AMWorldGen.RemoveItemFromWorldChests( ItemID.LivingMahoganyWand );
			AMWorldGen.RemoveItemFromWorldChests( ItemID.LivingMahoganyLeafWand );
			AMWorldGen.RemoveItemFromWorldChests( ItemID.LivingMahoganyLeafWand );

			if( config.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				AMWorldGen.AddItemToWorldChests(
					itemType: ModContent.ItemType<MountableMagicMirrorTileItem>(),
					quantity: 1,
					chancePerChest: config.WorldGenAddedMountedMagicMirrorChance
				);
			}

			AMWorldGen.ReplaceItemWithOrbsInWorldChests( ItemID.ClimbingClaws );
			AMWorldGen.ReplaceItemWithOrbsInWorldChests( ItemID.HerbBag );
			AMWorldGen.ReplaceItemWithOrbsInWorldChests( ItemID.FiberglassFishingPole );

			//

			var chestDef = new ChestTypeDefinition( TileID.Containers, null );
			float chestPotionMul = config.WorldGenChestPotionMultiplier;

			// Double all potions in world chests
			foreach( Chest chest in chestDef.GetMatchingWorldChests() ) {
				foreach( Item item in chest.item ) {
					if( item?.active != true || (!item.potion && (item.healLife == 0 && item.healMana == 0)) ) {
						continue;
					}

					item.stack = (int)((float)item.stack * chestPotionMul);
				}
			}
		}
	}
}
