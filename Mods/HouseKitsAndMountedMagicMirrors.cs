using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using MountedMagicMirrors.Tiles;
using PrefabKits;



namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadHouseKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			PrefabKitsAPI.OnPostHouseCreate( (tileX, tileY, item) => {
				var myworld = ModContent.GetInstance<AdventureModeWorld>();

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				PrefabKitsConfig.Instance.OverlayChanges( new PrefabKitsConfig {
					CustomFurnitureTile = furnDef.TileType,
				} );

				myworld.HouseKitFurnitureIdx++;
			} );

			PrefabKitsConfig.Instance.OverlayChanges(
				new PrefabKitsConfig {
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
