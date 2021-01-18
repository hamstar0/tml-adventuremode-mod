using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Items.Attributes;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			this.AddCustomTooltips( item, tooltips );
			this.AddPriceTooltip( item, tooltips );
		}


		////////////////

		private void AddCustomTooltips( Item item, List<TooltipLine> tooltips ) {
			AMConfig config = AMConfig.Instance;
			string modName = "[c/FFFF88:" + AMMod.Instance.DisplayName + "] - ";

			//

			void addTip( string ctx, string desc ) {
				TooltipLine tip = new TooltipLine( this.mod, "AdventureMode"+ctx, modName + desc );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
			}

			//

			switch( item.type ) {
			case ItemID.Wood:
				addTip( "Wood", "May be used to craft framing planks" );
				break;
			case ItemID.WoodPlatform:
				if( AMConfig.Instance.MaxPlatformBridgeLength > 0 ) {
					addTip( "Platform", "Only placeable in short ledges attached to something solid" );
				}
				break;
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					addTip( "RoD", "Cannot be used while Chaos State is active" );
				}
				break;
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
			case ItemID.Chain:
				addTip( "Rope", "Can only be lowered, unless placed against walls" );
				break;
			case ItemID.MinecartTrack:
				addTip( "Track1", "Can only bridge gaps or be placed downwards" );
				addTip( "Track2", "May be used to craft track deployment kits" );
				break;
			default:
				/*if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
					addTip( "Chains", "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
				}*/
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					addTip( "MMM", "May be placed once, but NOT removed!" );
				} else if( ItemAttributeHelpers.IsGrapple(item) && AMConfig.Instance.GrappleOnlyWoodAndPlatforms ) {
					addTip( "Grapple", "Only works on wood and platforms" );
				}
				break;
			}
		}

		private void AddPriceTooltip( Item item, List<TooltipLine> tooltips ) {
			if( Main.npcShop == 0 && item.value > 0 ) {
				long unitSellValue = item.value / 5;
				long stackSellValue = unitSellValue * item.stack;

				string[] renderedSellValueDenoms = ItemMoneyHelpers.RenderMoneyDenominations( stackSellValue, true, true );
				string renderedSellValue = string.Join( ", ", renderedSellValueDenoms );

				string tipText = "Sells for " + renderedSellValue;

				if( item.stack > 1 ) {
					string[] renderedUnitSellValueDenoms = ItemMoneyHelpers.RenderMoneyDenominations( unitSellValue, false, true );
					string renderedUnitSellValue = string.Join( ", ", renderedUnitSellValueDenoms );

					tipText += " ("+renderedUnitSellValue+" each)";
				}

				var tip = new TooltipLine( this.mod, "AdventureModeValue", tipText );
				ItemInformationAttributeHelpers.AppendTooltip( tooltips, tip );
			}
		}
	}
}
