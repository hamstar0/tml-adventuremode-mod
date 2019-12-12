﻿using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using AdventureMode.Items;
using HouseKits.Items;
using MountedMagicMirrors.Items;
using Terraria;


namespace AdventureMode {
	public class ItemQuantityDefinition {
		public ItemDefinition Item { get; set; }
		public int Quantity { get; set; }



		////////////////

		public ItemQuantityDefinition() { }

		public ItemQuantityDefinition( int itemType, int quantity=1 ) {
			this.Item = new ItemDefinition( itemType );
			this.Quantity = quantity;
		}

		////////////////

		public Item GetItem() {
			Item item;
			item = new Item();
			item.SetDefaults( this.Item.Type );
			item.stack = this.Quantity;
			return item;
		}
	}




	public partial class AdventureModeConfig : StackableModConfig {
		public List<ItemQuantityDefinition> RaftBarrelContents { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( ItemID.Wood, 50 ),
			new ItemQuantityDefinition( ModContent.ItemType<FramingPlankItem>(), 50 ),
			new ItemQuantityDefinition( ModContent.ItemType<HouseFurnishingKitItem>() ),
			new ItemQuantityDefinition( ModContent.ItemType<HouseFurnishingKitItem>() ),
			new ItemQuantityDefinition( ModContent.ItemType<HouseFurnishingKitItem>() ),
			new ItemQuantityDefinition( ModContent.ItemType<HouseFramingKitItem>() ),
			new ItemQuantityDefinition( ModContent.ItemType<HouseFramingKitItem>() ),
			new ItemQuantityDefinition( ModContent.ItemType<MountableMagicMirrorTileItem>(), 3 ),
			new ItemQuantityDefinition( ItemID.MinecartTrack, 500 ),
		};
	}
}
