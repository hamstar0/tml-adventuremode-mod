using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.DataStructures;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsEntityGroups.Services.EntityGroups;
using ModLibsEntityGroups.Services.EntityGroups.Definitions;
using LockedAbilities.Items.Consumable;
using Ergophobia.Items.FramingPlank;
using Ergophobia.Items.TrackDeploymentKit;
using Necrotis.Items;
using AdventureMode.Items;


namespace AdventureMode.Logic {
	static partial class RecipeLogic {
		private static ISet<int> GetWhitelistedRecipes( ISet<int> additionalWhitelistedItemTypes ) {
			var config = AMConfig.Instance;

			var whitelistTypes = new HashSet<int>();
			var blacklistTypes = new HashSet<int>();

			//

			void unionGroup( ISet<int> itemTypes, string grpName ) {
				if( EntityGroups.TryGetItemGroup(grpName, out IReadOnlySet<int> roGrp) ) {
					itemTypes.UnionWith( roGrp );
				} else {
					throw new ModLibsException( "Could not load entity group "+grpName );
				}
			}

			//

			unionGroup( whitelistTypes, ItemGroupIDs.AnyAccessory );
			unionGroup( whitelistTypes, ItemGroupIDs.AnyAmmo );
			unionGroup( whitelistTypes, ItemGroupIDs.AnyOreEquipment );
			unionGroup( whitelistTypes, ItemGroupIDs.AnyNonOreCraftedEquipment );
			unionGroup( whitelistTypes, ItemGroupIDs.AnyOreBar );

			whitelistTypes.Add( ItemID.Bottle );
			whitelistTypes.Add( ItemID.BottledHoney );
			if( config.EnableAlchemyRecipes ) {
				unionGroup( whitelistTypes, ItemGroupIDs.AnyPotion );
				//whitelistTypes.Add( ItemID.Bottle );
				//whitelistTypes.Add( ItemID.BottledHoney );
				whitelistTypes.Add( ItemID.BottledWater );
			}

			//
			//whitelistTypes.Add( ItemID.EnchantedNightcrawler );
			//
			whitelistTypes.Add( ItemID.Silk );
			//
			whitelistTypes.Add( ItemID.CopperCoin );
			whitelistTypes.Add( ItemID.SilverCoin );
			whitelistTypes.Add( ItemID.GoldCoin );
			whitelistTypes.Add( ItemID.PlatinumCoin );
			//
			whitelistTypes.Add( ItemID.Chain );
			whitelistTypes.Add( ItemID.SilkRope );
			whitelistTypes.Add( ItemID.RopeCoil );
			whitelistTypes.Add( ItemID.SilkRopeCoil );
			whitelistTypes.Add( ItemID.VineRopeCoil );
			whitelistTypes.Add( ItemID.WebRopeCoil );
			//
			whitelistTypes.Add( ItemID.Snowball );
			whitelistTypes.Add( ItemID.StickyBomb );
			whitelistTypes.Add( ItemID.StickyDynamite );
			whitelistTypes.Add( ItemID.StickyGrenade );
			whitelistTypes.Add( ItemID.StickyGlowstick );
			whitelistTypes.Add( ItemID.BouncyBomb );
			whitelistTypes.Add( ItemID.BouncyDynamite );
			whitelistTypes.Add( ItemID.BouncyGrenade );
			whitelistTypes.Add( ItemID.BouncyGlowstick );
			whitelistTypes.Add( ItemID.ViciousPowder );
			whitelistTypes.Add( ItemID.VilePowder );
			whitelistTypes.Add( ModContent.ItemType<SeismicChargeItem>() );
			whitelistTypes.Add( ModContent.ItemType<ThermalChargeItem>() );
			whitelistTypes.Add( ModContent.ItemType<DefoliationChargeItem>() );
			//
			whitelistTypes.Add( ItemID.ManaCrystal );
			whitelistTypes.Add( ModContent.ItemType<DarkHeartItem>() );
			//
			whitelistTypes.Add( ModContent.ItemType<ResurfacePotionItem>() );
			whitelistTypes.Add( ModContent.ItemType<ElixirOfLifeItem>() );
			//
			whitelistTypes.Add( ItemID.Bowl );
			whitelistTypes.Add( ItemID.BowlofSoup );
			whitelistTypes.Add( ItemID.GrubSoup );
			whitelistTypes.Add( ItemID.PumpkinPie );
			whitelistTypes.Add( ItemID.CookedFish );
			whitelistTypes.Add( ItemID.CookedShrimp );
			whitelistTypes.Add( ItemID.Sashimi );
			//
			whitelistTypes.UnionWith( RecipeLogic.GetItemIDsForPlatforms() );
			whitelistTypes.Add( ItemID.Wood );
			whitelistTypes.Add( ItemID.MinecartTrack );
			whitelistTypes.Add( ItemID.BoosterTrack );
			whitelistTypes.Add( ItemID.TallGate );
			//whitelistTypes.Add( ItemID.Barrel );
			whitelistTypes.Add( ModContent.ItemType<FramingPlankItem>() );
			whitelistTypes.Add( ModContent.ItemType<TrackDeploymentKitItem>() );
			//
			whitelistTypes.Add( ItemID.Campfire );
			whitelistTypes.Add( ItemID.HeartLantern );
			whitelistTypes.Add( ItemID.MagicLantern );
			//
			whitelistTypes.Add( ItemID.Minecart );
			whitelistTypes.Add( ItemID.MinecartMech );

			whitelistTypes.Add( ItemID.CopperWatch );
			whitelistTypes.Add( ItemID.TinWatch );
			whitelistTypes.Add( ItemID.SilverWatch );
			whitelistTypes.Add( ItemID.TungstenWatch );
			whitelistTypes.Add( ItemID.GoldWatch );
			whitelistTypes.Add( ItemID.PlatinumWatch );
			whitelistTypes.Add( ItemID.GPS );
			whitelistTypes.Add( ItemID.FishFinder );
			whitelistTypes.Add( ItemID.GoblinTech );
			whitelistTypes.Add( ItemID.REK );
			whitelistTypes.Add( ItemID.PDA );
			whitelistTypes.Add( ItemID.CellPhone );

			if( config.EnableTorchRecipes ) {
				unionGroup( whitelistTypes, ItemGroupIDs.AnyWallTorch );
			}

			if( config.EnableBossItemRecipes ) {
				if( !config.EnableAlchemyRecipes ) {
					whitelistTypes.Add( ItemID.BottledHoney );	// Exception I guess
				}
				whitelistTypes.Add( ItemID.GoldCrown );
				whitelistTypes.Add( ItemID.PlatinumCrown );
				//
				whitelistTypes.Add( ItemID.SlimeCrown );
				if( config.EnableBiomedBossItemRecipes ) {
					whitelistTypes.Add( ItemID.SuspiciousLookingEye );	// Technically all biomes; see chests
					whitelistTypes.Add( ItemID.WormFood );
					whitelistTypes.Add( ItemID.BloodySpine );
					whitelistTypes.Add( ItemID.Abeemination );
				}
				whitelistTypes.Add( ItemID.MechanicalWorm );
				whitelistTypes.Add( ItemID.MechanicalSkull );
				whitelistTypes.Add( ItemID.MechanicalEye );
				whitelistTypes.Add( ItemID.CelestialSigil );

				whitelistTypes.Add( ItemID.GoblinBattleStandard );
				//whitelistTypes.Add( ItemID.SnowGlobe );
				//whitelistTypes.Add( ItemID.PumpkinMoonMedallion );
				//whitelistTypes.Add( ItemID.NaughtyPresent );
			}

			//

			if( !config.OverrideRecipeTileRequirements ) {
				whitelistTypes.Add( ItemID.Furnace );
				whitelistTypes.Add( ItemID.Hellforge );
				whitelistTypes.Add( ItemID.AdamantiteForge );
				whitelistTypes.Add( ItemID.TitaniumForge );
			}

			//

			whitelistTypes.UnionWith( additionalWhitelistedItemTypes );

			////

			if( !config.EnableBasicGrappleItemRecipes ) {
				blacklistTypes.Add( ItemID.GrapplingHook );
				blacklistTypes.Add( ItemID.AmethystHook );
				blacklistTypes.Add( ItemID.SapphireHook );
				blacklistTypes.Add( ItemID.TopazHook );
				blacklistTypes.Add( ItemID.EmeraldHook );
				blacklistTypes.Add( ItemID.RubyHook );
				blacklistTypes.Add( ItemID.DiamondHook );
			}

			unionGroup( blacklistTypes, ItemGroupIDs.AnyWorkbench );
			unionGroup( blacklistTypes, ItemGroupIDs.AnyFishingPole );

			////

			whitelistTypes.ExceptWith( blacklistTypes );

			return whitelistTypes;
		}


		////////////////

		private static ISet<int> GetItemIDsForPlatforms() {
			var platforms = new HashSet<int> {
				ItemID.BlueBrickPlatform,
				ItemID.BonePlatform,
				ItemID.BorealWoodPlatform,
				ItemID.CactusPlatform,
				ItemID.CrystalPlatform,
				ItemID.DynastyPlatform,
				ItemID.EbonwoodPlatform,
				ItemID.FleshPlatform,
				ItemID.FrozenPlatform,
				ItemID.GlassPlatform,
				ItemID.GoldenPlatform,
				ItemID.GranitePlatform,
				ItemID.GreenBrickPlatform,
				ItemID.HoneyPlatform,
				ItemID.LihzahrdPlatform,
				ItemID.LivingWoodPlatform,
				ItemID.MarblePlatform,
				ItemID.MartianPlatform,
				ItemID.MeteoritePlatform,
				ItemID.MushroomPlatform,
				ItemID.ObsidianPlatform,
				ItemID.PalmWoodPlatform,
				ItemID.PearlwoodPlatform,
				ItemID.PinkBrickPlatform,
				ItemID.PumpkinPlatform,
				ItemID.RichMahoganyPlatform,
				ItemID.ShadewoodPlatform,
				ItemID.SkywarePlatform,
				ItemID.SlimePlatform,
				ItemID.SpookyPlatform,
				ItemID.SteampunkPlatform,
				ItemID.TeamBlockBluePlatform,
				ItemID.TeamBlockGreenPlatform,
				ItemID.TeamBlockPinkPlatform,
				ItemID.TeamBlockRedPlatform,
				ItemID.TeamBlockWhitePlatform,
				ItemID.TeamBlockYellowPlatform,
				ItemID.WoodPlatform,
			};
			return platforms;
		}
	}
}
