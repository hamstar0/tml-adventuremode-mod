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
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.HouseFramingKitPrice), Item.buyPrice(gold: 20) );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.HouseFurnishingKitPrice), Item.buyPrice(gold: 8) );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.TrackDeploymentKitRecipeTile), -1 );
			prefabKitsConfig.SetOverride( nameof(PrefabKitsConfig.TrackDeploymentKitRecipeExtraIngredient), new Dictionary<ItemDefinition, int> {
				{ new ItemDefinition(ItemID.SilkRopeCoil), 30 },
				{ new ItemDefinition(ItemID.Chain), 30 },
				{ new ItemDefinition(ItemID.Minecart), 1 },
				{ new ItemDefinition(ItemID.WoodenBeam), 100 }
			} );
		}
	}
}
