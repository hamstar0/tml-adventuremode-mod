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
		public bool IsCreatingHouse { get; private set; }



		////////////////

		public void LoadHouseKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			HouseKitsAPI.SetCustomFurniture( TileID.Tables );
			HouseKitsAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>() );

			HouseKitsAPI.OnPreHouseCreate( (tileX, tileY, item) => {
				this.IsCreatingHouse = true;
				return true;
			} );

			HouseKitsAPI.OnPostHouseCreate( (tileX, tileY, item) => {
				var myworld = ModContent.GetInstance<AdventureModeWorld>();

				this.IsCreatingHouse = false;

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				HouseKitsAPI.SetCustomFurniture( furnDef.TileType );

				myworld.HouseKitFurnitureIdx++;
			} );

			HouseKitsConfig.Instance.OverlayChanges(
				new HouseKitsConfig {
					HouseFramingKitPrice = 200000,
					HouseFurnishingKitPrice = 100000,
				}
			);
		}
	}
}
