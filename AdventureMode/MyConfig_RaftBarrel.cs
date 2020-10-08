using System;
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




	public partial class AdventureModeConfig : ModConfig {
		public List<ItemQuantityDefinition> RaftBarrelContents { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 50 ),
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 50 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem) ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem) ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 5 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(TrackDeploymentKitItem), 12 )
		};
	}
}
