using System;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using HamstarHelpers.Services.Hooks.LoadHooks;
using Nihilism;
using Terraria.ModLoader;
using AdventureMode.Items;
using PrefabKits.Items;
using LockedAbilities.Items.Consumable;
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
			//NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.GrapplingHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.AmethystHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.TopazHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.SapphireHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.EmeraldHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.RubyHook), true );
			NihilismAPI.SetRecipeBlacklist2Entry( new ItemDefinition(ItemID.DiamondHook), true );

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
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.CopperCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.SilverCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.GoldCoin), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.PlatinumCoin), true );
			//
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
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.ManaCrystal), true );
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ModContent.ItemType<DarkHeartItem>() ), true );
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
			NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ModContent.ItemType<StaffOfGaiaItem>()), true );

			if( AdventureModeConfig.Instance.EnableMechBossItemRecipes ) {
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalWorm ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalSkull ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.MechanicalEye ), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition( ItemID.CelestialSigil ), true );
			}

			if( AdventureModeConfig.Instance.RemoveRecipeTileRequirements ) {
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Furnace), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.Hellforge), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.AdamantiteForge), true );
				NihilismAPI.SetRecipeWhitelistEntry( new ItemDefinition(ItemID.TitaniumForge), true );
			}

			//

			//NihilismAPI.SetItemBlacklistGroupEntry( "Any Placeable", true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WoodPlatform), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Rope), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.SilkRope), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.VineRope), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.WebRope), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.Chain), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MinecartTrack), true );

			////NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TinkerersWorkshop), true );
			////NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.MythrilAnvil), true );
			////NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.OrichalcumAnvil), true );
			////NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.AdamantiteForge), true );
			////NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.TitaniumForge), true );
			//NihilismAPI.SetItemWhitelistEntry( new ItemDefinition(ItemID.LunarCraftingStation), true );

			NihilismAPI.NihilateCurrentWorld( true );
		}
	}
}
