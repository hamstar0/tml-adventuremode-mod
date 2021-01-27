using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;


namespace AdventureMode {
	public class ScaleSetting {
		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float Scale { get; set; } = 1f;
	}




	public partial class AMConfig : ModConfig {
		public Dictionary<ItemDefinition, ScaleSetting> ShopPriceScales { get; set; } = new Dictionary<ItemDefinition, ScaleSetting> {
			// Special items
			{ new ItemDefinition(ItemID.RocketBoots), new ScaleSetting { Scale = 7f } },
			// Supplies
			{ new ItemDefinition(ItemID.LesserHealingPotion), new ScaleSetting { Scale = 2f } },
			{ new ItemDefinition(ItemID.Torch), new ScaleSetting { Scale = 2f } },
			// Ammo
			{ new ItemDefinition(ItemID.Grenade), new ScaleSetting { Scale = 3f } },
			{ new ItemDefinition(ItemID.WoodenArrow), new ScaleSetting { Scale = 5f } },
			{ new ItemDefinition(ItemID.MusketBall), new ScaleSetting { Scale = 5f } }
		};


		[Range( 0f, 100f )]
		[DefaultValue( 2.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ValueablesShopPriceScale { get; set; } = 2.5f;
	}
}
