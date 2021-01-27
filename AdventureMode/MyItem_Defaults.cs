using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
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
			var config = AMConfig.Instance;
			var itemDef = new ItemDefinition( item.type );

			if( config.ShopPriceScales.ContainsKey(itemDef) ) {
				item.value = (int)((float)item.value * config.ShopPriceScales[ itemDef ].Scale);
			} else if( AMItem.ItemIsValuable(item) ) {
				item.value = (int)((float)item.value * config.ValueablesShopPriceScale);
			}

			switch( item.type ) {
			case ItemID.ReaverShark:
				if( config.NerfReaverShark ) {
					item.pick = 50;
				}
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
