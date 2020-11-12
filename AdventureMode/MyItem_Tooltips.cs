using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Items.Attributes;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			string modName = "[c / FFFF88" + AMMod.Instance.DisplayName + ":] ";
			AMConfig config = AMConfig.Instance;
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.Wood:
				tip = new TooltipLine( this.mod, "AdventureModeWood", modName+"May be used to craft framing planks" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			case ItemID.WoodPlatform:
				if( AMConfig.Instance.MaxPlatformBridgeLength > 0 ) {
					tip = new TooltipLine( this.mod, "AdventureModePlatform", modName+"Only placeable in short ledges attached to something solid" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					tip = new TooltipLine( this.mod, "AdventureModeRoD", modName+"Cannot be used while Chaos State is active" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
			case ItemID.Chain:
				tip = new TooltipLine( this.mod, "AdventureModeRope", modName+"Can only be lowered, unless placed against walls" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			case ItemID.MinecartTrack:
				tip = new TooltipLine( this.mod, "AdventureModeTrack1", modName+"Can only bridge gaps or be placed downwards" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				tip = new TooltipLine( this.mod, "AdventureModeTrack2", modName+"May be used to craft track deployment kits" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			default:
				/*if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
					tip = new TooltipLine( this.mod, modName+"Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}*/
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					tip = new TooltipLine( this.mod, "AdventureModeMMM", modName+"May be placed once, but NOT removed!" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				if( ItemAttributeHelpers.IsGrapple(item) && AMConfig.Instance.GrappleOnlyWoodAndPlatforms ) {
					tip = new TooltipLine( this.mod, "AdventureModeGrapple", modName+"Only works on wood and platforms" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			}
		}
	}
}
