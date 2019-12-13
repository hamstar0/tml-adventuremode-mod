using MountedMagicMirrors.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureMode {
	class AdventureModeItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			if( AdventureModeConfig.Instance.NerfReaverShark ) {
				if( item.type == ItemID.ReaverShark ) {
					item.pick = 50;
				}
			}
		}

		////////////////

		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			AdventureModeConfig config = AdventureModeConfig.Instance;
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.WoodPlatform:
				if( AdventureModeConfig.Instance.MaxPlatformBridgeLength > 0 ) {
					tip = new TooltipLine( this.mod, "AdventureModePlatform", "Only placeable in short ledges attached to something solid" );
					tooltips.Add( tip );
				}
				break;
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					tip = new TooltipLine( this.mod, "AdventureModeRoD", "Cannot be used while Chaos State is active" );
					tooltips.Add( tip );
				}
				break;
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
				tip = new TooltipLine( this.mod, "AdventureModeCoil", "Can only be lowered" );
				tooltips.Add( tip );
				break;
			default:
				/*if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
					tip = new TooltipLine( this.mod, "AdventureModeGrapple", "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
					tooltips.Add( tip );
				}*/
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					tip = new TooltipLine( this.mod, "AdventureModeMMM", "May be placed once, but NOT removed!" );
					tooltips.Add( tip );
				}
				if( item.pick > 0 ) {
					tip = new TooltipLine( this.mod, "AdventureModePick", "Able to break ores, plants, gems, sand, silt, and wood" );
					tooltips.Add( tip );
				}
				break;
			}
		}


		////////////////
		
		public override void OnConsumeItem( Item item, Player player ) {
			switch( item.type ) {
			/*case ItemID.ManaCrystal:
				if( AdventureModeConfig.Instance.ReducedManaCrystalStatIncrease ) {
					player.statManaMax -= 15;
				}
				this.ModifyPopupText();
				break;*/	//<- Implemented via FindableManaCrystals mod
			case ItemID.LifeCrystal:
				if( AdventureModeConfig.Instance.ReducedLifeCrystalStatIncrease ) {
					player.statLifeMax -= 10;
					this.ModifyPopupText();
				}
				break;
			}
		}


		////////////////

		private void ModifyPopupText() {
			for( int idx = 0; idx < Main.combatText.Length; idx++ ) {
				CombatText txt = Main.combatText[idx];
				if( txt == null || !txt.active ) { continue; }

				if( txt.text.Equals( "20" ) ) {
					txt.text = "10";
					break;
				}
			}
		}
	}
}
