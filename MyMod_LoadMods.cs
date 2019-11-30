using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ObjectData;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HamstarHelpers.Services.Hooks.LoadHooks;
using ChestImplants;
using TheTrickster;
using HouseFurnishingKit;
using LockedAbilities;
using LockedAbilities.Items.Consumable;
using MountedMagicMirrors.Tiles;
using MountedMagicMirrors.Items;
using Nihilism;


namespace AdventureMode {
	partial class AdventureModeMod : Mod {
		private void LoadNihilism() {
			EntityGroups.Enable();

			NihilismAPI.InstancedFiltersOn();
			NihilismAPI.OnSyncOrWorldLoad( ( isSync ) => {
				if( isSync && LoadHelpers.IsWorldBeingPlayed() ) {
					return;
				}

				LoadHooks.AddPostWorldLoadOnceHook( () => {
					this.ApplyNihilismFilters();
				} );
			}, 0f );
		}


		private void LoadChestImplants() {
			var implantsConfig = new ChestImplantsConfig();
			implantsConfig.AllFromSetChestImplanterDefinitions.Clear();
			implantsConfig.RandomPickFromSetChestImplanterDefinitions.Clear();

			if( AdventureModeConfig.Instance.WorldGenRemoveMagicMirrors ) {
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

			if( AdventureModeConfig.Instance.WorldGenAddedMountedMagicMirrorChance > 0f ) {
				implantsConfig.RandomPickFromSetChestImplanterDefinitions[ "AdventureModeMountedMirrors" ]
					= new ChestImplanterSetDefinition { new ChestImplanterDefinition {
						ChestTypes = new HashSet<string> { "Vanilla Underground World Chest" },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ModContent.ItemType<MountableMagicMirrorTileItem>() ),
								MinQuantity = 1,
								MaxQuantity = 1,
								ChancePerChest = 0.05f,
							}
						}
				} };
			}

			ChestImplantsConfig.Instance.OverlayChanges( implantsConfig );
		}


		private void LoadHouseFurnishingKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			HouseFurnishingKitAPI.SetCustomFurniture( TileID.Tables, 3, 2 );
			HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );

			HouseFurnishingKitAPI.OnHouseCreate( (tileX, tileY, item) => {
				if( this.HouseKitFurnitureCycleIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[ this.HouseKitFurnitureCycleIdx ];
				TileObjectData furnTileObj = TileObjectData.GetTileData( furnDef.TileType, 0 );
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				int width = furnTileObj?.Width ?? 1;
				int height = furnTileObj?.Height ?? 1;

				HouseFurnishingKitAPI.SetCustomFurniture( furnDef.TileType, width, height );

				this.HouseKitFurnitureCycleIdx++;
			} );
		}

		
		private void LoadTricksterAndLockedAbilies() {
			ModConfigStack.SetStackedConfigChangesOnly( new TheTricksterConfig {
				DropsOnDefeat = new ItemDefinition( ModContent.ItemType<DarkHeartPieceItem>() )
			} );
		}


		private void LoadLockedAbilities() {
			if( AdventureModeConfig.Instance.WorldGenRemoveDarkHeartPieces ) {
				var config = new LockedAbilitiesConfig {
					WorldGenChestImplantDarkHeartPieceChance = 0f
				};

				ModConfigStack.SetStackedConfigChangesOnly( config );
			}

			LockedAbilitiesConfig.Instance.OverlayChanges( new LockedAbilitiesConfig {
				BackBraceEnabled = false,
				WorldGenChestImplantBackBraceChance = 0f
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
