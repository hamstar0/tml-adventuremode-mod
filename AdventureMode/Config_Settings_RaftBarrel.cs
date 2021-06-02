using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using ModLibsGeneral.Libraries.World;
using Ergophobia.Items.FramingPlank;
using Ergophobia.Items.HouseFurnishingKit;
using Ergophobia.Items.HouseFramingKit;
using Ergophobia.Items.TrackDeploymentKit;
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

		public bool ScaleQuantityByWorldSize { get; set; } = false;



		////////////////

		public ItemQuantityDefinition() { }

		public ItemQuantityDefinition(
					string mod,
					string name,
					int quantity,
					bool scaleQuantityByWorldSize=false,
					float weight=1f ) {
			this.Item = new ItemDefinition( mod, name );
			this.Quantity = quantity;
			this.ScaleQuantityByWorldSize = scaleQuantityByWorldSize;
			this.Weight = weight;
		}

		public ItemQuantityDefinition( ItemQuantityDefinition clone ) {
			this.Item = new ItemDefinition( clone.Item.mod, clone.Item.name );
			this.Quantity = clone.Quantity;
			this.ScaleQuantityByWorldSize = clone.ScaleQuantityByWorldSize;
			this.Weight = clone.Weight;
		}

		////////////////

		public Item GetItem() {
			Item item = new Item();
			item.SetDefaults( this.Item.Type, true );

			item.stack = this.Quantity;

			if( this.ScaleQuantityByWorldSize ) {
				switch( WorldLibraries.GetSize() ) {
				case WorldSize.SubSmall:
					item.stack = (int)((float)item.stack * 0.75f);
					break;
				//case WorldSize.Small:
				//	break;
				case WorldSize.Medium:
					item.stack = (int)((float)item.stack * 2f);
					break;
				case WorldSize.Large:
					item.stack = (int)((float)item.stack * 3f);
					break;
				case WorldSize.SuperLarge:
					item.stack = (int)((float)item.stack * 4f);
					break;
				}
			}

			return item;
		}
	}




	public partial class AMConfig : ModConfig {
		public List<ItemQuantityDefinition> RaftBarrelContents { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 50 ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 50 ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(FramingPlankItem), 50 ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(HouseFurnishingKitItem), 3 ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(HouseFramingKitItem), 2 ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 5, true ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(TrackDeploymentKitItem), 12, true )
		};

		[Range( 0, 60 * 25 * 30 )]
		[DefaultValue( 60 * 25 * 3 )]
		public int RaftBarrelRestockSecondsDuration { get; set; } = 60 * 25 * 3;  // 3 days
		
		public List<ItemQuantityDefinition> RaftBarrelRestockSelection { get; set; } = new List<ItemQuantityDefinition> {
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Wood), 25, false, 2f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WoodPlatform), 25, false, 1f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Rope), 50, false, 1f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.Torch), 25, false, 1f ),
			//
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.ArcheryPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.BattlePotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.CalmingPotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.EndurancePotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.FeatherfallPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.FlipperPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.GillsPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.GravitationPotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.HunterPotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.InfernoPotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.IronskinPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.LifeforcePotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.MagicPowerPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.ManaRegenerationPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.MiningPotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.NightOwlPotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.ObsidianSkinPotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.RagePotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.RecallPotion), 3, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.RegenerationPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.ShinePotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.SpelunkerPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.SummoningPotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.SwiftnessPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.TeleportationPotion), 1, false, 0.125f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.ThornsPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.TitanPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.TrapsightPotion), 2, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WarmthPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WaterWalkingPotion), 1, false, 0.25f ),
			new ItemQuantityDefinition( nameof(Terraria), nameof(ItemID.WrathPotion), 1, false, 0.125f ),
			//
			new ItemQuantityDefinition( nameof(AdventureMode), nameof(FramingPlankItem), 25, false, 1f ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(HouseFurnishingKitItem), 1, false, 1f ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(HouseFramingKitItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(MountedMagicMirrors), nameof(MountableMagicMirrorTileItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Ergophobia), nameof(TrackDeploymentKitItem), 2, false, 1f ),
			//
			new ItemQuantityDefinition( nameof(Orbs), nameof(BlueOrbItem), 1, false, 0.5f ),
			//new ItemQuantityDefinition( nameof(Orbs), nameof(BrownOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(CyanOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(GreenOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(PinkOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(PurpleOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(RedOrbItem), 1, false, 0.5f ),
			new ItemQuantityDefinition( nameof(Orbs), nameof(YellowOrbItem), 1, false, 0.5f ),
			//new ItemQuantityDefinition( nameof(Orbs), nameof(WhiteOrbItem), 1, false, 0.5f ),
		};
	}
}
