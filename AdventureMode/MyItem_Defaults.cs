using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.DataStructures;
using HamstarHelpers.Services.EntityGroups;
using HamstarHelpers.Services.EntityGroups.Definitions;


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
	}
}
