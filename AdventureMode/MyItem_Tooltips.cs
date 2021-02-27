using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Items.Attributes;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			this.AddCustomTooltips( item, tooltips );
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
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					addTip( "RoD", "Cannot be used while Chaos State is active" );
				}
				break;
			case ItemID.StrangePlant1:
			case ItemID.StrangePlant2:
			case ItemID.StrangePlant3:
			case ItemID.StrangePlant4:
				addTip( "StrangePlant", "Important: This item is now used as an ingredient for boss summon items" );
				break;
			default:
				//if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
				//	addTip( "Chains", "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
				//}
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					addTip( "MMM", "Warning: May be placed once, but NOT removed" );
				} else if( ItemAttributeHelpers.IsGrapple(item) && AMConfig.Instance.GrappleOnlyWoodAndPlatforms ) {
					addTip( "Grapple", "Only works on wood and platforms" );
				}
				break;
			}
		}
	}
}
