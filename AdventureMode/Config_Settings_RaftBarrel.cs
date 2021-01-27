﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using AdventureMode.Items;
using PrefabKits.Items;
using MountedMagicMirrors.Items;
using Orbs.Items;


namespace AdventureMode {
	public class ItemQuantityDefinition {
		public ItemDefinition Item { get; set; }

		[Range(1, 999)]
		[DefaultValue( 1 )]
		public int Quantity { get; set; } = 1;

		[Range( 0.01f, 99f )]
		[DefaultValue( 1f )]
		public float Weight { get; set; } = 1f;



		////////////////

		public ItemQuantityDefinition() { }

		public ItemQuantityDefinition( string mod, string name, int quantity, float weight=1f ) {
			this.Item = new ItemDefinition( mod, name );
			this.Quantity = quantity;
			this.Weight = weight;
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
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem), 1 ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 5 ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(TrackDeploymentKitItem), 12 )
		};

		[Range( 0, 60 * 25 * 30 )]
		[DefaultValue( 60 * 25 * 3 )]
		public int RaftBarrelRestockSecondsDuration { get; set; } = 60 * 25 * 3;  // 3 days
		
		public List<ItemQuantityDefinition> RaftBarrelRestockSelection { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 25, 2f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 25, 1f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Rope), 50, 1f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Torch), 25, 1f ),
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 25, 1f ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFurnishingKitItem), 1, 1f ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(HouseFramingKitItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(PrefabKits), nameof(TrackDeploymentKitItem), 2, 1f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(BlueOrbItem), 1, 0.5f ),
			//new ItemQuantityDefinition( nameof(Orbs), nameof(BrownOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(CyanOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(GreenOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(PinkOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(PurpleOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(RedOrbItem), 1, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(YellowOrbItem), 1, 0.5f ),
			//new ItemQuantityDefinition( nameof(Orbs), nameof(WhiteOrbItem), 1, 0.5f ),
		};
	}
}
