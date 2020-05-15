using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Helpers.Debug;
using PrefabKits.Items;


namespace AdventureMode {
	public partial class AdventureModeConfig : StackableModConfig {
		//[ReloadRequired]
		public Dictionary<NPCDefinition, List<ItemDefinition>> ShopWhitelists { get; set; } = new Dictionary<NPCDefinition, List<ItemDefinition>> {
			{
				new NPCDefinition( NPCID.Merchant ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.CopperPickaxe ),
					new ItemDefinition( ItemID.CopperAxe ),
					new ItemDefinition( ItemID.MiningHelmet ),
					new ItemDefinition( ItemID.BugNet ),
					new ItemDefinition( ItemID.Torch ),
					new ItemDefinition( ItemID.LesserHealingPotion ),
					//new ItemDefinition( ItemID.LesserManaPotion ),
					new ItemDefinition( ItemID.WoodenArrow ),
					new ItemDefinition( ItemID.Shuriken ),
					new ItemDefinition( ItemID.Rope ),
					new ItemDefinition( ItemID.Marshmallow ),
					new ItemDefinition( ItemID.ThrowingKnife ),
					new ItemDefinition( ItemID.Glowstick ),
					new ItemDefinition( ItemID.Flare ),
					new ItemDefinition( ItemID.BlueFlare ),
					//new ItemDefinition( ItemID.Sickle ),
					new ItemDefinition( ItemID.PiggyBank ),
					new ItemDefinition( ItemID.Safe ),
					new ItemDefinition( ItemID.GoldDust ),
					new ItemDefinition( ItemID.Nail ),
					new ItemDefinition( ItemID.TaxCollectorHat ),
					new ItemDefinition( ItemID.TaxCollectorSuit ),
					new ItemDefinition( ItemID.TaxCollectorPants ),
					new ItemDefinition( ModContent.ItemType<HouseFramingKitItem>() ),
					new ItemDefinition( ModContent.ItemType<HouseFurnishingKitItem>() ),
				}
			},
			{
				new NPCDefinition( NPCID.GoblinTinkerer ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.RocketBoots ),
					new ItemDefinition( ItemID.Ruler ),
					new ItemDefinition( ItemID.GrapplingHook ),
					new ItemDefinition( ItemID.Toolbelt ),
					new ItemDefinition( ItemID.SpikyBall ),
				}
			},
			{
				new NPCDefinition( NPCID.Dryad ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.PurificationPowder ),
					new ItemDefinition( ItemID.VilePowder ),
					new ItemDefinition( ItemID.ViciousPowder ),
					new ItemDefinition( ItemID.GrassSeeds ),
					new ItemDefinition( ItemID.CorruptSeeds ),
					new ItemDefinition( ItemID.CrimsonSeeds ),
					new ItemDefinition( ItemID.Sunflower ),
					new ItemDefinition( ItemID.Acorn ),
					//new ItemDefinition( ItemID.DirtRod ),
					new ItemDefinition( ItemID.PumpkinSeed ),
					new ItemDefinition( ItemID.HallowedSeeds ),
					new ItemDefinition( ItemID.MushroomGrassSeeds ),
					new ItemDefinition( ItemID.DryadCoverings ),
					new ItemDefinition( ItemID.DryadLoincloth ),
					//new ItemDefinition( ModContent.ItemType<StaffOfGaiaItem>() ),
					//new ItemDefinition( ModContent.ItemType<GeoResonantOrbItem>() ),
				}
			},
			{
				new NPCDefinition( NPCID.Mechanic ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.MechanicalEye ),
					new ItemDefinition( ItemID.EngineeringHelmet ),
					new ItemDefinition( ItemID.MechanicsRod ),
				}
			},
			{
				new NPCDefinition( NPCID.WitchDoctor ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.Blowgun ),
					new ItemDefinition( ItemID.Stinger ),
					new ItemDefinition( ItemID.Stake ),
					new ItemDefinition( ItemID.TikiTotem ),
					new ItemDefinition( ItemID.LeafWings ),
					new ItemDefinition( ItemID.VialofVenom ),
					new ItemDefinition( ItemID.TikiMask ),
					new ItemDefinition( ItemID.TikiShirt ),
					new ItemDefinition( ItemID.TikiPants ),
					new ItemDefinition( ItemID.PygmyNecklace ),
					new ItemDefinition( ItemID.HerculesBeetle ),
					new ItemDefinition( ItemID.ImbuingStation ),
					new ItemDefinition( ItemID.BewitchingTable ),
				}
			}
		};
	}
}
