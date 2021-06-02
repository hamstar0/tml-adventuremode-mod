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
		private void AddItemToWorldChests( int itemType, int quantity, float chancePerChest = 1f ) {
			var fillDef = new ChestFillDefinition(
				single: new ChestFillItemDefinition( itemType, quantity, quantity ),
				percentChance: chancePerChest
			);

			WorldChestLibraries.AddToWorldChests( fillDef );
		}

		
		private void RemoveItemFromWorldChests( int itemType, float chancePerChest = 1f ) {
			var fillDef = new ChestFillDefinition(
				single: new ChestFillItemDefinition( itemType, 1, 1 ),
				percentChance: chancePerChest
			);

			WorldChestLibraries.RemoveFromWorldChests( fillDef );
		}

		
		private void ReplaceItemWithOrbsInWorldChests( int itemType ) {  //string chestType="Chest"
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


		////

		public void LoadChestImplants() {
			var config = AMConfig.Instance;

			if( !config.EnableAlchemyRecipes ) {
				this.RemoveItemFromWorldChests( ItemID.Bottle );
			}
			if( config.WorldGenRemoveMagicMirrors ) {
				this.RemoveItemFromWorldChests( ItemID.MagicMirror );
				this.RemoveItemFromWorldChests( ItemID.IceMirror );
			}
			this.RemoveItemFromWorldChests( ItemID.LivingWoodWand );
			this.RemoveItemFromWorldChests( ItemID.LeafWand );
			this.RemoveItemFromWorldChests( ItemID.LivingMahoganyWand );
			this.RemoveItemFromWorldChests( ItemID.LivingMahoganyLeafWand );
			this.RemoveItemFromWorldChests( ItemID.LivingMahoganyLeafWand );

			if( config.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				this.AddItemToWorldChests(
					itemType: ModContent.ItemType<MountableMagicMirrorTileItem>(),
					quantity: 1,
					chancePerChest: config.WorldGenAddedMountedMagicMirrorChance
				);
			}

			this.ReplaceItemWithOrbsInWorldChests( ItemID.ClimbingClaws );
			this.ReplaceItemWithOrbsInWorldChests( ItemID.HerbBag );
			this.ReplaceItemWithOrbsInWorldChests( ItemID.FiberglassFishingPole );

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
