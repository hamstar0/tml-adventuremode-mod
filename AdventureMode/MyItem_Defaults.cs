using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.DataStructures;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;


namespace AdventureMode {
	partial class AdventureModeItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			switch( item.type ) {
			case ItemID.ReaverShark:
				if( AdventureModeConfig.Instance.NerfReaverShark ) {
					item.pick = 50;
				}
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
				if( item.createTile == 0 ) {
					break;
				}

				if( item.createTile == TileID.MetalBars ) {
					item.value *= 2;
				} else if( item.createTile > 0 && item.createTile < TileID.Sets.Ore.Length && TileID.Sets.Ore[item.createTile] ) {
					item.value *= 2;
				} else {
					if( EntityGroups.IsLoaded ) {
						IReadOnlySet<int> grp;
						EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyOreBar, out grp );  // Modded bar?
						//EntityGroups.TryGetItemGroup( ItemGroupIDs.AnyVanillaGem, out grp );
						if( grp.Contains( item.type ) ) {
							item.value *= 2;
						}
					}
				}
				break;
			}
		}
	}
}
