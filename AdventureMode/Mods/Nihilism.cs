using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HamstarHelpers.Services.Hooks.LoadHooks;
using Nihilism;
using AdventureMode.Items;
using PrefabKits.Items;
using LockedAbilities.Items.Consumable;
using LockedAbilities.Items.Accessories;
using StaffOfGaia.Items;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadNihilism() {
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


		////////////////

		private void ApplyNihilismFilters() {
			NihilismAPI.ClearFiltersForCurrentWorld( true );

			//

			NihilismAPI.SetRecipeBlacklistGroupEntry( "Any Item", true );

			//

			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyAmmo, true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyOreEquipment, true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyNonOreCraftedEquipment, true );
			NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyOreBar, true );

			if( AdventureModeConfig.Instance.EnableAlchemyRecipes ) {
				NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyPotion, true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Bottle), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BottledHoney), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BottledWater), true );
			}

			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.EnchantedNightcrawler), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CopperCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.SilverCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.GoldCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.PlatinumCoin), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Chain), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.SilkRope), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.RopeCoil), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.SilkRopeCoil), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.VineRopeCoil), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.WebRopeCoil), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Snowball), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.StickyBomb), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.StickyDynamite), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.StickyGrenade), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.StickyGlowstick), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BouncyBomb), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BouncyDynamite), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BouncyGrenade), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BouncyGlowstick), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.ViciousPowder), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.VilePowder), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.ManaCrystal), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<DarkHeartItem>()), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Bowl), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BowlofSoup), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.GrubSoup), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.PumpkinPie), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CookedFish), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CookedShrimp), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Sashimi), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Wood), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MinecartTrack), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BoosterTrack), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.TallGate), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<FramingPlankItem>()), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<TrackDeploymentKitItem>()), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Campfire), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.HeartLantern), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MagicLantern), true );
			//
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.GrapplingHook), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Minecart), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.MinecartMech), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<StaffOfGaiaItem>()), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<UtilitarianBeltItem>()), true );

			if( AdventureModeConfig.Instance.EnableTorchRecipes ) {
				NihilismAPI.SetRecipeWhitelistGroupEntry( ItemGroupIDs.AnyWallTorch, true );
			}
			if( AdventureModeConfig.Instance.EnableBossItemRecipes ) {
				if( !AdventureModeConfig.Instance.EnableAlchemyRecipes ) {
					NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.BottledHoney), true );
				}
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.GoldCrown), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.PlatinumCrown), true );
				//
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.SlimeCrown ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.SuspiciousLookingEye ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.WormFood ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.BloodySpine ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.Abeemination ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalWorm ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalSkull ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalEye ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.CelestialSigil ), true );
			}

			//

			if( AdventureModeConfig.Instance.RemoveRecipeTileRequirements ) {
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Furnace), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Hellforge), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.AdamantiteForge), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.TitaniumForge), true );
			}

			NihilismAPI.NihilateCurrentWorld( true );
		}
	}
}
