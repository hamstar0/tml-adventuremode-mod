using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.DataStructures;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;


namespace AdventureMode.Logic {
	static partial class ItemLogic {
		private static ISet<int> ValueableItemBlacklist = new HashSet<int> {
			ItemID.DemoniteOre,
			ItemID.DemoniteBar,
			ItemID.CrimtaneOre,
			ItemID.CrimtaneBar,
			ItemID.HallowedBar,
		};



		////////////////

		public static bool ItemIsValuable( Item item ) {
			if( ItemLogic.ValueableItemBlacklist.Contains(item.type) ) {
				return false;
			}

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

		public static bool ApplyShopPriceRespecIf( Item item ) {
			var config = AMConfig.Instance;
			var itemDef = new ItemDefinition( item.type );

			if( !config.ShopPriceScales.ContainsKey( itemDef ) ) {
				return false;
			}

			item.value = (int)( (float)item.value * config.ShopPriceScales[itemDef].Scale );
			return true;
		}

		public static bool ApplyValuablesPriceRespecIf( Item item ) {
			var config = AMConfig.Instance;

			if( !ItemLogic.ItemIsValuable( item ) ) {
				return false;
			}

			item.value = (int)( (float)item.value * config.ValueablesShopPriceScale );
			return true;
		}
	}
}
