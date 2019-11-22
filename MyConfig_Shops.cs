using HamstarHelpers.Services.Configs;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace AdventureMode {
	public partial class AdventureModeConfig : StackableModConfig {
		public Dictionary<NPCDefinition, List<ItemDefinition>> ShopWhitelists { get; set; } = new Dictionary<NPCDefinition, List<ItemDefinition>> {
			{
				new NPCDefinition( NPCID.Merchant ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.MiningHelmet ),
					new ItemDefinition( ItemID.BugNet ),
					//new ItemDefinition( ItemID.Torch ),
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
					new ItemDefinition( ItemID.Sickle ),
					new ItemDefinition( ItemID.GoldDust ),
					new ItemDefinition( ItemID.Nail ),
					new ItemDefinition( ItemID.TaxCollectorHat ),
					new ItemDefinition( ItemID.TaxCollectorSuit ),
					new ItemDefinition( ItemID.TaxCollectorPants ),
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
				new NPCDefinition( NPCID.Mechanic ), new List<ItemDefinition> {
					new ItemDefinition( ItemID.MechanicalEye ),
					new ItemDefinition( ItemID.EngineeringHelmet ),
					new ItemDefinition( ItemID.MechanicsRod ),
				}
			}
		};
	}
}
