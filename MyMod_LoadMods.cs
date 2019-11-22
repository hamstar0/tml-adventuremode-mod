using ChestImplants;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HouseFurnishingKit;
using LockedAbilities.Items.Consumable;
using MountedMagicMirrors.Tiles;
using Nihilism;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using TheTrickster;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		private void LoadNihilism() {
			EntityGroups.Enable();
			NihilismAPI.InstancedFiltersOn();
			NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync ) { return; }
				this.ApplyNihilismFilters();
			}, 0f );
		}


		private void LoadChestImplants() {
			var mirrorGoneDef = new ChestImplanterDefinition {
				ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
				ItemDefinitions = new List<ChestImplanterItemDefinition> {
					new ChestImplanterItemDefinition {
						ChestItem = new ItemDefinition( ItemID.MagicMirror ),
						MinQuantity = -1,
						MaxQuantity = -1,
						ChancePerChest = 1f,
					}
				}
			};
			var mirrorGoneSetDef = new ChestImplanterSetDefinition { mirrorGoneDef };

			ChestImplantsMod.Config.ChestImplanterDefinitions.Clear();
			ChestImplantsMod.Config.ChestImplanterDefinitions["AdventureModeMirrorGone"] = mirrorGoneSetDef;
		}


		private void LoadHouseFurnishingKit() {
			this.FurnitureCycle = new (ushort, int, int)[] {
				(TileID.Anvils, 2, 1),
				(TileID.Furnaces, 3, 2),
				(TileID.CookingPots, 2, 2),
				(TileID.Containers, 2, 2),
				(TileID.Bottles, 1, 1),
				(TileID.TinkerersWorkbench, 3, 2),
				(TileID.PiggyBank, 2, 1),
				(TileID.Statues, 2, 3)
			};

			HouseFurnishingKitAPI.OnHouseCreate( (tileX, tileY, item) => {
				if( this.FurnitureCycleIdx >= this.FurnitureCycle.Length ) { return; }

				var furniture = this.FurnitureCycle[ this.FurnitureCycleIdx++ ];
				HouseFurnishingKitAPI.SetCustomFurniture( furniture.TileType, furniture.Width, furniture.Height );
				HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );
			} );

			HouseFurnishingKitAPI.SetCustomFurniture( TileID.Tables, 3, 2 );
			HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );
			HouseFurnishingKitAPI.SetCustomWallMount2( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );
		}

		
		private void LoadTrickster() {
			StackableModConfig.SetConfig<TheTricksterConfig>( new TheTricksterConfig {
				DropsOnDefeat = new ItemDefinition( ModContent.ItemType<DarkHeartItem>() )
			} );
		}


		////////////////

		private void ApplyNihilismFilters() {
			NihilismAPI.ClearFiltersForCurrentWorld( true );

			NihilismAPI.SetRecipeBlacklistGroupEntry( "Any Item", true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyOreEquipment, true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyNonOreCraftedEquipment, true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.ManaCrystal), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BoosterTrack), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalWorm), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalSkull), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MechanicalEye), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CelestialSigil), true );

			/*NihilismAPI.SetItemBlacklistGroupEntry( "Any Placeable", true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Rope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.SilkRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.VineRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WebRope), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Chain), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MinecartTrack), true );

			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TinkerersWorkshop), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MythrilAnvil), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.OrichalcumAnvil), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.AdamantiteForge), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TitaniumForge), true );
			NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.LunarCraftingStation), true );*/

			NihilismAPI.NihilateCurrentWorld( true );
		}
	}
}
