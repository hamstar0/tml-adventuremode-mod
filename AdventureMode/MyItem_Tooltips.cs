using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items.Attributes;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			AMConfig config = AMConfig.Instance;
			string modName = "[c/FFFF88:" + AMMod.Instance.DisplayName + "] - ";

			//

			void addTip( string ctx, string desc ) {
				TooltipLine tip = new TooltipLine( this.mod, "AdventureMode"+ctx, modName + desc );
				ItemInformationAttributeLibraries.ApplyTooltipAt( tooltips, tip );
			}

			//

			switch( item.type ) {
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					addTip( "RoD", "Cannot be used while Chaos State is active" );
				}
				break;
			case ItemID.LifeCrystal:
				if( config.ReducedLifeCrystalStatIncrease ) {
					int idx = tooltips.FindIndex( t => t.Name == "LifeCrystal" );

					if( idx >= 0 ) {
						tooltips[idx] = new TooltipLine(
							this.mod,
							"AdventureModeLifeCrysta",
							modName + "Permanently increases maximum life by 10"
						);
					} else {
						addTip( "LifeCrystal", "Now adds only +10 max life." );
					}
				}
				break;
			case ItemID.StrangePlant1:
			case ItemID.StrangePlant2:
			case ItemID.StrangePlant3:
			case ItemID.StrangePlant4:
				addTip( "StrangePlant", "Important: Ingredient for boss summon items" );
				break;
			default:
				//if( config.GrappleChainAmmoRate > 0 && ItemAttributeLibraries.IsGrapple( item ) ) {
				//	addTip( "Chains", "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
				//}
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					addTip( "MMM", "Warning: May be placed once, but NOT removed" );
				}
				break;
			}
		}
	}
}
