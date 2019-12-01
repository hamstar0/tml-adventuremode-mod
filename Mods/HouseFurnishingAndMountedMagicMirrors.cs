using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using HamstarHelpers.Helpers.Debug;
using HouseFurnishingKit;
using MountedMagicMirrors.Tiles;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		private int HouseKitFurnitureCycleIdx = 0;



		////////////////

		public void LoadHouseFurnishingKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			HouseFurnishingKitAPI.SetCustomFurniture( TileID.Tables, 3, 2 );
			HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>(), 3, 3 );

			HouseFurnishingKitAPI.OnHouseCreate( (tileX, tileY, item) => {
				if( this.HouseKitFurnitureCycleIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[ this.HouseKitFurnitureCycleIdx ];
				TileObjectData furnTileObj = TileObjectData.GetTileData( furnDef.TileType, 0 );
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				int width = furnTileObj?.Width ?? 1;
				int height = furnTileObj?.Height ?? 1;

				HouseFurnishingKitAPI.SetCustomFurniture( furnDef.TileType, width, height );

				this.HouseKitFurnitureCycleIdx++;
			} );
		}
	}
}
