using ChestImplants;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HouseFurnishingKit;
using LockedAbilities;
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
				if( isSync && LoadHelpers.IsWorldBeingPlayed() ) {
					return;
				}
				this.ApplyNihilismFilters();
			}, 0f );
		}


		private void LoadChestImplants() {
			var implantsConfig = new ChestImplantsConfig();
			implantsConfig.AllFromSetChestImplanterDefinitions.Clear();
			implantsConfig.RandomPickFromSetChestImplanterDefinitions.Clear();

			if( AdventureModeConfig.Instance.RemoveWorldGenMagicMirrors ) {
				var mirrorGoneDef1 = new ChestImplanterDefinition {
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
				var mirrorGoneDef2 = new ChestImplanterDefinition {
					ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.IceMirror ),
							MinQuantity = -1,
							MaxQuantity = -1,
							ChancePerChest = 1f,
						}
					}
				};

				implantsConfig.AllFromSetChestImplanterDefinitions[ "AdventureModeMirrorGone" ]
					= new ChestImplanterSetDefinition { mirrorGoneDef1, mirrorGoneDef2 };
			}

			ModConfigStack.SetStackedConfig( implantsConfig );
		}


		private void LoadHouseFurnishingKitAndMountedMagicMirrors() {
			this.HouseKitFurnitureCycle = new (ushort, int, int, bool)[] {
				(TileID.Anvils, 2, 1, false),
				(TileID.Furnaces, 3, 2, false),
				(TileID.CookingPots, 2, 2, false),
				(TileID.Anvils, 2, 1, false),
				(TileID.Furnaces, 3, 2, false),
				//(TileID.Bottles, 1, 1, false),
				(TileID.Sawmill, 3, 3, false),
				(TileID.TinkerersWorkbench, 3, 2, false),
				(TileID.PiggyBank, 2, 1, false),
				//(TileID.Statues, 2, 3, false),
				(TileID.MythrilAnvil, 2, 1, true),
				(TileID.AdamantiteForge, 3, 2, true),
				(TileID.Safes, 2, 2, true),
			};

			HouseFurnishingKitAPI.SetCustomFurniture( TileID.Tables, 3, 2 );
			HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );

			HouseFurnishingKitAPI.OnHouseCreate( (tileX, tileY, item) => {
				if( this.HouseKitFurnitureCycleIdx >= this.HouseKitFurnitureCycle.Length ) {
					return;
				}

				var furniture = this.HouseKitFurnitureCycle[ this.HouseKitFurnitureCycleIdx ];
				if( furniture.IsHardMode && !Main.hardMode ) {
					return;
				}

				HouseFurnishingKitAPI.SetCustomFurniture( furniture.TileType, furniture.Width, furniture.Height );

				this.HouseKitFurnitureCycleIdx++;
			} );
		}

		
		private void LoadTricksterAndLockedAbilies() {
			ModConfigStack.SetStackedConfigChangesOnly( new TheTricksterConfig {
				DropsOnDefeat = new ItemDefinition( ModContent.ItemType<DarkHeartPieceItem>() )
			} );
		}


		private void LoadLockedAbilities() {
			if( AdventureModeConfig.Instance.RemoveWorldGenDarkHeartPieces ) {
				return;
			}

			var config = new LockedAbilitiesConfig {
				WorldGenChestImplantDarkHeartPieceChance = 0f
			};

			if( AdventureModeConfig.Instance.RemoveWorldGenDarkHeartPieces ) {
			}

			ModConfigStack.SetStackedConfigChangesOnly( config );
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
			if( AdventureModeConfig.Instance.EnableMechBossItemRecipes ) {
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalWorm ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalSkull ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalEye ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.CelestialSigil ), true );
			}

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
