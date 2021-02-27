using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureMode.Logic;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			var config = AMConfig.Instance;

			if( !ItemLogic.ApplyShopPriceRespecIf(item) ) {
				ItemLogic.ApplyValuablesPriceRespecIf( item );
			}

			switch( item.type ) {
			case ItemID.ReaverShark:
				if( config.NerfReaverShark ) {
					item.pick = 50;
				}
				break;
			/*//case ItemID.CorruptSeeds:
			//case ItemID.CrimsonSeeds:
			//case ItemID.HallowedSeeds:
			//case ItemID.JungleGrassSeeds:
			//case ItemID.GrassSeeds:
			//case ItemID.MushroomGrassSeeds:
			case ItemID.BlinkrootSeeds:
			case ItemID.DaybloomSeeds:
			case ItemID.DeathweedSeeds:
			case ItemID.FireblossomSeeds:
			case ItemID.MoonglowSeeds:
			case ItemID.ShiverthornSeeds:
			case ItemID.WaterleafSeeds:
				if( !config.EnableAlchemyRecipes ) {
					item.TurnToAir();
				}
				break;*/
			}
		}
	}
}
