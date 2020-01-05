using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using MountedMagicMirrors.Tiles;
using PrefabKits;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadPrefabsKitAndMountedMagicMirrors() {
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
					CustomFurnitureTile = TileID.Containers,
					CustomWallMount1Tile = (ushort)ModContent.TileType<MountedMagicMirrorTile>(),
					CustomFloorTile = TileID.Mudstone,
					HouseFramingKitPrice = 100000,
					HouseFurnishingKitPrice = 100000,
					TrackDeploymentKitRecipeTile = -1,
					TrackDeploymentKitRecipeExtraIngredient = new List<ItemDefinition>()
				}
			);
		}
	}
}
