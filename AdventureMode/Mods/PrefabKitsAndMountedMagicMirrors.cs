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
			var prefabKitsConfig = PrefabKitsConfig.Instance;
			IList<HouseKitFurnitureDefinition> cycle = AMConfig.Instance.HouseKitFurnitureSuccession;

			PrefabKitsAPI.OnPostHouseCreate( (tileX, tileY) => {
				var myworld = ModContent.GetInstance<AMWorld>();

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.CustomFurnitureTile), furnDef.TileType );

				myworld.HouseKitFurnitureIdx++;
			} );

			ushort mirrorTileType = (ushort)ModContent.TileType<MountedMagicMirrorTile>();

			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.CustomFurnitureTile), TileID.Containers );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.CustomWallMount1Tile), mirrorTileType );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.CustomFloorTile), TileID.Mudstone );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.HouseFramingKitPrice), 100000 );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.HouseFurnishingKitPrice), 100000 );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.TrackDeploymentKitRecipeTile), -1 );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.TrackDeploymentKitRecipeExtraIngredient), new List<ItemDefinition>() );
		}
	}
}
