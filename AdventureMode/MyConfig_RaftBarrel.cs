using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using AdventureMode.Items;
using PrefabKits.Items;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	public class ItemQuantityDefinition {
		public ItemDefinition Item { get; set; }

		public int Quantity { get; set; }



		////////////////

		public ItemQuantityDefinition() { }

		public ItemQuantityDefinition( string mod, string name, int quantity=1 ) {
			this.Item = new ItemDefinition( mod, name );
			this.Quantity = quantity;
		}

		////////////////

		public Item GetItem() {
			Item item = new Item();
			item.SetDefaults( this.Item.Type, true );
			item.stack = this.Quantity;
			return item;
		}
	}




	public partial class AMConfig : ModConfig {
		public List<ItemQuantityDefinition> RaftBarrelContents { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 50 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 50 ),
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 50 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem) ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 5 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(TrackDeploymentKitItem), 12 )
		};

		[Range( 0, 60 * 25 * 30 )]
		[DefaultValue( 60 * 25 * 3 )]
		public int RaftBarrelRestockSecondsDuration { get; set; } = 60 * 25 * 3;  // 3 days

		public List<ItemQuantityDefinition> RaftBarrelRestockSelection { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 25 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 25 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 25 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 25 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Rope), 50 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Rope), 50 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Torch), 25 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Torch), 25 ),
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 25 ),
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 25 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(TrackDeploymentKitItem), 2 )
		};
	}
}
