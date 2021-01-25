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
			var pkConfig = PrefabKitsConfig.Instance;
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

				pkConfig.SetOverride( nameof(pkConfig.CustomFurnitureTile), furnDef.TileType );

				myworld.HouseKitFurnitureIdx++;
			} );

			ushort mirrorTileType = (ushort)ModContent.TileType<MountedMagicMirrorTile>();

			pkConfig.SetOverride( nameof(pkConfig.CustomFurnitureTile), TileID.Containers );
			pkConfig.SetOverride( nameof(pkConfig.CustomWallMount1Tile), mirrorTileType );
			pkConfig.SetOverride( nameof(pkConfig.CustomFloorTile), TileID.Mudstone );
			pkConfig.SetOverride( nameof(pkConfig.HouseFramingKitPrice), Item.buyPrice(gold: 20) );
			pkConfig.SetOverride( nameof(pkConfig.HouseFurnishingKitPrice), Item.buyPrice(gold: 8) );
			pkConfig.SetOverride( nameof(pkConfig.TrackDeploymentKitRecipeTile), -1 );
			pkConfig.SetOverride( nameof(pkConfig.TrackDeploymentKitRecipeExtraIngredient), new Dictionary<ItemDefinition, int> {
				//{ new ItemDefinition(ItemID.SilkRopeCoil), 30 },
				{ new ItemDefinition(ItemID.Chain), 15 },
				//{ new ItemDefinition(ItemID.Minecart), 1 },
				//{ new ItemDefinition(ItemID.WoodenBeam), 50 }
			} );
		}
	}
}
