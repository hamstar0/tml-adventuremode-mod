using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Helpers.Tiles;
using MountedMagicMirrors.Items;
using Orbs.Items;


namespace AdventureMode.WorldGeneration {
	partial class AMWorldGen {
		public void LoadChestImplants() {
			void addItemImplanter( int itemType, int quantity, float chancePerChest=1f ) {
				var fillDef = new ChestFillDefinition(
					single: new ChestFillItemDefinition(itemType, quantity, quantity),
					percentChance: chancePerChest
				);

				WorldChestHelpers.AddToWorldChests( fillDef );
			}

			void addOrbImplanterReplacer( int itemType ) {  //string chestType="Chest"
				var unfillDef = new ChestFillDefinition( new ChestFillItemDefinition(itemType, 1, 1) );
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
				var chestDef = new ChestTypeDefinition( TileID.Containers, TileFrameHelpers.PlainChestFrame );

				IList<Chest> modifiedChests = WorldChestHelpers.RemoveFromWorldChests( unfillDef, chestDef );
				foreach( Chest chest in modifiedChests ) {
					orbFillDef.Fill( chest );
				}
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

			addOrbImplanterReplacer( ItemID.ClimbingClaws );
			addOrbImplanterReplacer( ItemID.HerbBag );
		}
	}
}
