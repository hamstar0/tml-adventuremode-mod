using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.DataStructures;
using HamstarHelpers.Helpers.Items.Attributes;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;
using MountedMagicMirrors.Items;


namespace AdventureMode {
	partial class AdventureModeItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			if( AdventureModeConfig.Instance.NerfReaverShark ) {
				switch( item.type ) {
				case ItemID.ReaverShark:
					item.pick = 50;
					break;
				case ItemID.RocketBoots:
					if( AdventureModeConfig.Instance.RocketBootsCost >= 0 ) {
						item.value = AdventureModeConfig.Instance.RocketBootsCost;
					}
					break;
				case ItemID.LesserHealingPotion:
					item.value *= 2;
					break;
				case ItemID.Torch:
					item.value *= 2;
					break;
				case ItemID.Grenade:
					item.value *= 2;
					break;
				default:
					if( item.createTile > 0 ) {
						if( item.createTile == TileID.MetalBars || TileID.Sets.Ore[item.createTile] ) {
							item.value *= 2;
						} else {
							IReadOnlySet<int> grp;

							EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyOreBar, out grp );	// Modded bar?
							if( grp.Contains( item.type ) ) {
								item.value *= 2;
							}

							/*EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyVanillaGem, out grp );
							if( grp.Contains( item.type ) ) {
								item.value *= 2;
							}*/
						}
					}
					break;
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
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			case ItemID.RodofDiscord:
				if( config.RodOfDiscordChaosStateBlocksBlink ) {
					tip = new TooltipLine( this.mod, "AdventureModeRoD", "Cannot be used while Chaos State is active" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			case ItemID.Rope:
			case ItemID.SilkRope:
			case ItemID.VineRope:
			case ItemID.WebRope:
			case ItemID.Chain:
				tip = new TooltipLine( this.mod, "AdventureModeCoil", "Can only be lowered" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			case ItemID.MinecartTrack:
				tip = new TooltipLine( this.mod, "AdventureModeTrack", "Can only bridge gaps or be placed downwards" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			default:
				/*if( config.GrappleChainAmmoRate > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
					tip = new TooltipLine( this.mod, "Consumes " + config.GrappleChainAmmoRate + " chain(s) per use" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}*/
				if( item.type == ModContent.ItemType<MountableMagicMirrorTileItem>() ) {
					tip = new TooltipLine( this.mod, "AdventureModeMMM", "May be placed once, but NOT removed!" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				if( ItemAttributeHelpers.IsGrapple(item) && AdventureModeConfig.Instance.GrappleOnlyWoodAndPlatforms ) {
					tip = new TooltipLine( this.mod, "AdventureModeGrapple", "Only works on wood and platforms" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
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
