using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using MountedMagicMirrors.Tiles;
using Ergophobia;


namespace AdventureMode.Mods {
	partial class AdventureModeModInteractions {
		public void LoadErgophobiaAndMountedMagicMirrors() {
			var ergConfig = ErgophobiaConfig.Instance;
			IList<HouseKitFurnitureDefinition> cycle = AMConfig.Instance.HouseKitFurnitureSuccession;

			ErgophobiaAPI.OnPostHouseCreate( (tileX, tileY) => {
				var myworld = ModContent.GetInstance<AMWorld>();

				if( myworld.HouseKitFurnitureIdx >= cycle.Count ) {
					return;
				}

				HouseKitFurnitureDefinition furnDef = cycle[myworld.HouseKitFurnitureIdx];
				if( furnDef.IsHardMode && !Main.hardMode ) {
					return;
				}

				ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomFurnitureTile), (int)furnDef.TileType );

				myworld.HouseKitFurnitureIdx++;
			} );

			ergConfig.SetOverride( nameof(ergConfig.TrackKitSoldByMerchant), false );

			ushort mirrorTileType = (ushort)ModContent.TileType<MountedMagicMirrorTile>();
			string mirrorUid = TileID.GetUniqueKey( mirrorTileType );
			List<string> wl = ergConfig.Get<List<string>>( nameof(ergConfig.TilePlaceWhitelist) )
				?? ergConfig.TilePlaceWhitelist.ToList();
			
			if( !wl.Contains(mirrorUid) ) {
				wl.Add( mirrorUid );
			}

			ergConfig.SetOverride( nameof(ergConfig.TilePlaceWhitelist), wl );

			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomFurnitureTile), TileID.Containers );
			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomWallMount1Tile), mirrorTileType );
			ergConfig.SetOverride( nameof(ergConfig.FurnishedCustomFloorTile), TileID.Mudstone );
			ergConfig.SetOverride( nameof(ergConfig.HouseFramingKitPrice), Item.buyPrice(gold: 20) );
			ergConfig.SetOverride( nameof(ergConfig.HouseFurnishingKitPrice), Item.buyPrice(gold: 8) );
			ergConfig.SetOverride( nameof(ergConfig.TrackDeploymentKitRecipeTile), -1 );
			ergConfig.SetOverride( nameof(ergConfig.TrackDeploymentKitRecipeExtraIngredient), new Dictionary<ItemDefinition, int> {
				//{ new ItemDefinition(ItemID.SilkRopeCoil), 30 },
				{ new ItemDefinition(ItemID.Chain), 15 },
				//{ new ItemDefinition(ItemID.Minecart), 1 },
				//{ new ItemDefinition(ItemID.WoodenBeam), 50 }
			} );
		}
	}
}
