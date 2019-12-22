using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using MountedMagicMirrors.Tiles;
using HouseKits;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadHouseKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			HouseKitsAPI.OnPostHouseCreate( (tileX, tileY, item) => {
				var myworld = ModContent.GetInstance<AdventureModeWorld>();

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				HouseKitsConfig.Instance.OverlayChanges( new HouseKitsConfig {
					CustomFurnitureTile = furnDef.TileType,
				} );

				myworld.HouseKitFurnitureIdx++;
			} );

			HouseKitsConfig.Instance.OverlayChanges(
				new HouseKitsConfig {
					CustomFurnitureTile = TileID.Tables,
					CustomWallMount1Tile = (ushort)ModContent.TileType<MountedMagicMirrorTile>(),
					CustomFloorTile = TileID.Mudstone,
					HouseFramingKitPrice = 200000,
					HouseFurnishingKitPrice = 100000,
				}
			);
		}
	}
}
