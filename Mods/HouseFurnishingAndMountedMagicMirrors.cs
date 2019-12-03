using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HouseFurnishingKit;
using MountedMagicMirrors.Tiles;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public bool IsCreatingHouse { get; private set; }



		////////////////

		public void LoadHouseFurnishingKitAndMountedMagicMirrors() {
			IList<HouseKitFurnitureDefinition> cycle = AdventureModeConfig.Instance.HouseKitFurnitureSuccession;

			HouseFurnishingKitAPI.SetCustomFurniture( TileID.Tables );
			HouseFurnishingKitAPI.SetCustomWallMount1( (ushort)ModContent.TileType<MountedMagicMirrorTile>() );

			HouseFurnishingKitAPI.OnPreHouseCreate( (tileX, tileY, item) => {
				this.IsCreatingHouse = true;
				return true;
			} );

			HouseFurnishingKitAPI.OnPostHouseCreate( (tileX, tileY, item) => {
				var myworld = ModContent.GetInstance<AdventureModeWorld>();

				this.IsCreatingHouse = false;

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}
				
				HouseFurnishingKitAPI.SetCustomFurniture( furnDef.TileType );

				myworld.HouseKitFurnitureIdx++;
			} );
		}
	}
}
