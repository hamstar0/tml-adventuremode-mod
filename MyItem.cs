using HamstarHelpers.Helpers.Items.Attributes;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			AdventureModeConfig config = AdventureModeConfig.Instance;
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.WoodPlatform:
				tip = new TooltipLine( this.mod, "AdventureModePlatform", "Only placeable in short ledges attached to something solid" );
				tooltips.Add( tip );
				break;
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					tip = new TooltipLine( this.mod, "AdventureModeRodOfDiscord", "Cannot be used while Chaos State is active" );
					tooltips.Add( tip );
				}
				break;
			default:
				if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
					tip = new TooltipLine( this.mod, "AdventureModeGrapple", "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
					tooltips.Add( tip );
				}
				break;
			}
		}


		////////////////
		
		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.ManaCrystal:
				if( AdventureModeConfig.Instance.ReducedManaCrystalStatIncrease ) {
					player.statManaMax -= 15;
				}
				break;
			case ItemID.LifeCrystal:
				if( AdventureModeConfig.Instance.ReducedLifeCrystalStatIncrease ) {
					player.statLifeMax -= 15;
				}
				break;
			}
		}
	}
}
