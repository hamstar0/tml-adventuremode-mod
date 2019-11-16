using HamstarHelpers.Helpers.Items.Attributes;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.WoodPlatform:
				tip = new TooltipLine( this.mod, "AdventureModePlatform", "Only placeable in short ledges attached to something solid" );
				tooltips.Add( tip );
				break;
			}

			if( ItemAttributeHelpers.IsGrapple(item) ) {
				AdventureModeConfig config = AdventureModeMod.Config;

				tip = new TooltipLine( this.mod, "AdventureModeGrapple", "Consumes "+config.GrappleChainAmmoRate+" chain(s) per use" );
				tooltips.Add( tip );
			}
		}


		////////////////

		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.ManaCrystal:
				player.statManaMax -= 15;
				break;
			case ItemID.LifeCrystal:
				player.statLifeMax -= 15;
				break;
			}
		}
	}
}
