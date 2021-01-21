using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.DataStructures;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;


namespace AdventureMode {
	partial class AMItem : GlobalItem {
		public static bool ItemIsValuable( Item item ) {
			if( item.createTile == TileID.MetalBars ) {
				return true;
			}

			if( item.createTile > 0 && item.createTile < TileID.Sets.Ore.Length && TileID.Sets.Ore[item.createTile] ) {
				return true;
			}

			if( EntityGroups.IsLoaded ) {
				IReadOnlySet<int> grp;

				EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyOreBar, out grp );  // Modded bar?
				if( grp?.Contains( item.type ) ?? false ) {
					return true;
				}

				EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyVanillaGem, out grp );
				if( grp?.Contains( item.type ) ?? false ) {
					return true;
				}
			}

			return false;
		}



		////////////////

		public override void SetDefaults( Item item ) {
			switch( item.type ) {
			case ItemID.ReaverShark:
				if( AMConfig.Instance.NerfReaverShark ) {
					item.pick = 50;
				}
				break;
			case ItemID.RocketBoots:
				if( AMConfig.Instance.RocketBootsCost >= 0 ) {
					item.value = AMConfig.Instance.RocketBootsCost;
				}
				break;
			case ItemID.LesserHealingPotion:
				item.value *= 2;
				break;
			case ItemID.Torch:
				item.value *= 2;
				break;
			// Jack up ammo prices
			case ItemID.Grenade:
				item.value *= 3;
				break;
			case ItemID.WoodenArrow:
				item.value *= 5;
				break;
			case ItemID.MusketBall:
				item.value *= 5;
				break;
			default:
				if( AMItem.ItemIsValuable(item) ) {
					item.value *= 3;
				}
				break;
			}
		}
	}
}
